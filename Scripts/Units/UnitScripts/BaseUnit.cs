using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public UnitConfig config;
    public TeamConfiguration teamConfig;
    public Canvas healthBarCanvas;
    public RectTransform healthBarFill;
    private float regenTimer;
    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = config.maxHealth;
        regenTimer = config.RegenerationSpeed;
        UpdateHealthBar();

        // If we have a visual representation in the config, apply it.
        // Assuming a SpriteRenderer for simplicity, but this can be more complex.
        //if (config.visualRepresentation)
        //{
        //    GetComponent<SpriteRenderer>().sprite = config.visualRepresentation;
        //}
    }
    protected virtual void Update()
    {
        HandleRegeneration();
    }

    protected void HandleRegeneration()
    {
        if (currentHealth < config.maxHealth && regenTimer <= 0f)
        {
            // Restore health and reset the regen timer.
            currentHealth += config.Regeneration;
            currentHealth = Mathf.Clamp(currentHealth, 0, config.maxHealth);
            regenTimer = config.RegenerationSpeed;

            UpdateHealthBar();
        }
        regenTimer -= Time.deltaTime; // Decrease the regen timer continuously.
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        float effectiveDamage = CalculateEffectiveDamage(damage, damageType);
        currentHealth -= effectiveDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, config.maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private float CalculateEffectiveDamage(float damage, DamageType damageType)
    {
        float resistancePercentage = 0f;

        switch (damageType)
        {
            case DamageType.Physical:
                resistancePercentage = config.PhysicalResistPercentage;
                break;
            case DamageType.Fire:
                resistancePercentage = config.FireResistPercentage;
                break;
            case DamageType.Ice:
                resistancePercentage = config.IceResistPercentage;
                break;
            case DamageType.Lightning:
                resistancePercentage = config.LightningResistPercentage;
                break;
        }

        // Apply resistance: damage * (1 - resistance percentage)
        return damage * (1f - resistancePercentage);
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    protected void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float healthPercentage = currentHealth / config.maxHealth;
            healthBarFill.localScale = new Vector3(healthPercentage, 1f, 1f);
        }
    }
    private void OnEnable()
    {
        if (teamConfig != null)
        {
            UnitManager.instance.RegisterUnit(this, teamConfig);
        }
    }
    private void OnDisable()
    {
        if (teamConfig != null)
        {
            UnitManager.instance.UnregisterUnit(this, teamConfig);
        }
    }
    public void SetTeamConfiguration(TeamConfiguration newConfig)
    {
        teamConfig = newConfig;
    }
}

    public void Attack(BaseUnit target)
    {
        // Check if the target is not null
        if (target != null)
        {
            // Calculate the damage based on whether the unit is melee or ranged
            float damage = 0f;
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= config.TrueRange) // Melee attack
            {
                damage = config.MeleeDamage;
            }
            else if (distanceToTarget <= config.TrueRange) // Ranged attack
            {
                damage = config.RangedAttack;
            }

            // Apply the damage to the target
            if (damage > 0f)
            {
                target.TakeDamage(damage, config.DamageType);
            }
        }
    }
