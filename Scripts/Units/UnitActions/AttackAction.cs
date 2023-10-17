using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override float Evaluate(AIContext context)
    {
        // Check if the target enemy is within range and the AI unit is not retreating
        if (context.targetUnit != null && !context.retreating && Vector3.Distance(context.selfUnit.transform.position, context.targetUnit.transform.position) <= context.range)
        {
            // If both conditions are met, return a high priority value
            return 1.0f;
        }
        else
        {
            // Otherwise, return a low priority value
            return 0.0f;
        }
    }
    public override void Execute(AIContext context)
    {
        // Check if the target is not null
        if (context.targetUnit != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (context.targetUnit.transform.position - context.selfUnit.transform.position).normalized;

            // Rotate the AI unit to face the target
            context.selfUnit.transform.rotation = Quaternion.LookRotation(direction);

            // Attack the target
            context.selfUnit.Attack(context.targetUnit);
        }
    }
}

