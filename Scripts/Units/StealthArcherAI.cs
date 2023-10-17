using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StealthArcherAI : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float detectionRadius;
    public float safeDistance;
    // Variables to store observations and references to components.

    private void Update()
    {
        float moveToTargetUtility = CalculateMoveToTargetUtility();
        float attackTargetUtility = CalculateAttackTargetUtility();
        float retreatUtility = CalculateRetreatUtility();
        float hideUtility = CalculateHideUtility();
        float supportAllyUtility = CalculateSupportAllyUtility();

        // Add more action utilities as needed.

        // Determine the highest utility action.
        float highestUtility = Mathf.Max(new float[] { moveToTargetUtility, attackTargetUtility,
                                                      retreatUtility, hideUtility, supportAllyUtility });

        // Execute the corresponding action.
        if (highestUtility == moveToTargetUtility) MoveToTarget();
        else if (highestUtility == attackTargetUtility) AttackTarget();
        else if (highestUtility == retreatUtility) Retreat();
        else if (highestUtility == hideUtility) Hide();
        else if (highestUtility == supportAllyUtility) SupportAlly();

        // Add more action executions as needed.
    }

    float CalculateMoveToTargetUtility()
    {
        float utility = 0f;

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // The closer the target, the higher the utility to move to it. Adjust as needed.
            utility = 1f / distanceToTarget;
        }

        // Consider additional factors like line of sight, stealth paths, etc.
        // ...

        return utility;
    }

    float CalculateAttackTargetUtility()
    {
        float utility = 0f;

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // If within attacking distance and target is not noticing the archer, high utility.
            if (distanceToTarget < detectionRadius) // Assume a suitable attack range
            {
                utility = 2f; // Arbitrary higher value indicating a good opportunity to attack.
            }
        }

        return utility;
    }

    float CalculateMoveToTargetUtility()
    {
        // Consider factors like distance, line of sight, and stealth paths.
        // Return the calculated utility.
    }

    // Define other CalculateUtility() methods similarly.

    void MoveToTarget()
    {
        // Implement the logic for moving towards the target.
    }

    float CalculateHideUtility()
    {
        // Factors influencing hiding might be current health, number of enemies noticing the archer, etc.

        // Example: If the archer is noticed by enemies, increase hiding utility
        float utility = IsArcherNoticedByEnemy() ? 1f : 0f;
        return utility;
    }

    bool IsArcherNoticedByEnemy()
    {
        // Use a Physics check to find all enemies within a certain radius, and check their "vision" towards the archer.
        Collider[] enemiesInRadius = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        foreach (Collider enemy in enemiesInRadius)
        {
            Vector3 toArcher = transform.position - enemy.transform.position;
            if (Vector3.Dot(toArcher.normalized, enemy.transform.forward) > 0.7f) // Assuming a 90° field of view
            {
                // Additional line-of-sight check might be needed here using Raycasting.
                return true;
            }
        }
        return false;
    }

    void Hide()
    {
        // Logic to find a safe spot away from enemies.
        Vector3 safeSpot = FindSafeSpot();
        MoveTo(safeSpot);
    }

    Vector3 FindSafeSpot()
    {
        // Basic example: Move to a spot opposite to the nearby enemy.
        // More advanced logic might consider multiple enemies, terrain, etc.

        Collider[] enemiesInRadius = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        if (enemiesInRadius.Length == 0) return transform.position; // Already safe.

        Vector3 toEnemy = enemiesInRadius[0].transform.position - transform.position;
        Vector3 runDirection = -toEnemy.normalized;
        Vector3 safeSpot = transform.position + (runDirection * safeDistance);
        return safeSpot;
    }

    void MoveTo(Vector3 destination)
    {
        // Logic to move the archer to the specified destination.

        // Example logic:
        // navMeshAgent.SetDestination(destination);
    }
    // Define other Action() methods similarly.
}