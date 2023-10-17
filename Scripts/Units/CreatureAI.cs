using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureAI : MonoBehaviour
{
    private float detectionRadius;
    public float attackCooldown;
    public float attackRange;
    public LayerMask enemyLayers;

    private NavMeshAgent agent;
    private float attackTimer = 0f;
    private BaseUnit targetUnit;
    private BaseUnit myUnit;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myUnit = GetComponent<BaseUnit>();
        agent.speed = myUnit.config.TrueSpeed;
        detectionRadius = myUnit.config.VisionRange;
        attackCooldown = myUnit.config.AttackSpeed;
        // Determine enemy layer based on this unit's layer.
        SetEnemyLayer();
        UnitManager.OnUnitSpawnedOrDestroyed += UpdateActiveUnits;
        UpdateActiveUnits();
    }

    private void SetEnemyLayer()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Team6"))
        {
            enemyLayers = 1 << LayerMask.NameToLayer("Team7");
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Team7"))
        {
            enemyLayers = 1 << LayerMask.NameToLayer("Team6");
        }
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        // If no target or target is dead, search for a new target.
        if (targetUnit == null || targetUnit.gameObject.activeInHierarchy == false)
        {
            SearchForTarget();
        }

        // If a target is available and in range, attack if cooldown allows.
        if (targetUnit != null &&
            Vector3.Distance(transform.position, targetUnit.transform.position) <= myUnit.config.TrueRange &&
            attackTimer <= 0f)
        {
            Attack(targetUnit);
        }
        // If target is available but out of attack range, chase it.
        else if (targetUnit != null)
        {
            agent.SetDestination(targetUnit.transform.position);
        }
    }

    private void SearchForTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayers);
        Debug.Log($"{name} found {hits.Length} potential targets."); // Check how many potential targets found.
        if (hits.Length > 0)
        {
            // Note: Optimally, you'd want to find the closest enemy, not just the first found.
            Transform enemy = hits[0].transform;
            targetUnit = enemy.GetComponent<BaseUnit>();
        }
        else
        {
            targetUnit = null;
        }
    }
    private void Attack(BaseUnit target)
    {
        Debug.Log($"{name} is attempting to attack {target.name}");
        if (attackTimer <= 0f)
        {
            bool isCritical = Random.Range(0f, 100f) < myUnit.config.CriticalChance;

            // Get a damage value between damage/2 and damage*2.
            float damage = Random.Range(myUnit.config.MeleeDamage / 2f, myUnit.config.MeleeDamage * 2f);

            // Apply critical multiplier if it's a critical hit.
            if (isCritical)
            {
                damage *= myUnit.config.CriticalMutiplier;
                Debug.Log("Critical Hit!");
            }
            if (target != null)
            {
                target.TakeDamage(damage, DamageType.Physical);
            }
            attackTimer = attackCooldown;
        }
    }
    private void OnDestroy()
    {
        UnitManager.OnUnitSpawnedOrDestroyed -= UpdateActiveUnits;
    }
    private void UpdateActiveUnits()
    {
        context.activeAllies = new List<BaseUnit>(UnitManager.instance.activeAllies);
        context.activeEnemies = new List<BaseUnit>(UnitManager.instance.activeEnemies);
    }
}
