public class RetreatingAction : AIAction
{
    public override float Evaluate(AIContext context)
    {
        // Check if the recent damage is high and the current health is low
        if (context.recentDamage > context.selfUnit.config.maxHealth * 0.5f && context.selfUnit.currentHealth < context.selfUnit.config.maxHealth * 0.3f)
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
        // Set the destination of the AI unit's NavMeshAgent to the spawn point
        context.selfUnit.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = context.spawnPoint;
    }
}
