using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForTargetAction : AIAction
{
    public float detectionRadius;
    public LayerMask enemyLayers;

    public override float Evaluate(AIContext context)
    {
        // If we don't have a target, we should definitely search for one.
        if (context.targetUnit == null)
        {
            return 1f; // Max utility
        }
        else
        {
            // If we have a target but it's too far, consider finding a new one.
            float distanceToTarget = Vector3.Distance(context.selfUnit.transform.position, context.targetUnit.transform.position);
            float maxSearchDistance = 20f; // Example value

            // If the distance is more than max, return max utility.
            if (distanceToTarget > maxSearchDistance)
            {
                return 1f;
            }
            else
            {
                // Return a scaled value based on distance. The farther the current target, the higher the utility of searching for a new target.
                // This will return a value between 0 and 1, with it being closer to 1 the farther away the target is.
                return distanceToTarget / maxSearchDistance;
            }
        }
    }

    public override void Execute(AIContext context)
    {
        Collider[] hits = Physics.OverlapSphere(context.selfUnit.transform.position, detectionRadius, enemyLayers);
        if (hits.Length > 0)
        {
            Transform enemy = hits[0].transform;
            context.targetUnit = enemy.GetComponent<BaseUnit>();
        }
        else
        {
            context.targetUnit = null;
        }
    }
}

