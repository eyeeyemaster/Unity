using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public AISelector selector;
    public AIContext context;
    private float actionTimer = 0f; // Timer to keep track of cooldown.
    public BaseUnit myUnit;

    private void Start()
    {
        context.selfUnit = GetComponent<BaseUnit>();
        myUnit = context.selfUnit;
        selector = new AISelector
        {
            actions = new List<AIAction> {
                new AttackAction(),
                new ChaseAction(),
                new SearchForTargetAction()
            
                // Add more actions as needed...
            }
        };
    }

    private void Update()
    {
        // The logic for determining the target, dealing with cooldowns, etc., 
        // can be moved to the Evaluate() method of your AIActions.
        // Your existing logic for finding a target can be used here or refactored into actions...
        // Reduce the timer as time passes.
        actionTimer -= Time.deltaTime;

        // Only evaluate and execute actions if cooldown has elapsed.
        if (actionTimer <= 0f)
        {
            EvaluateAndExecuteActions();
            actionTimer = myUnit.config.AttackSpeed;
        }
    }
    private void EvaluateAndExecuteActions()
    {
        context.activeAllies = GetAllActiveAllies();
        context.activeEnemies = GetAllActiveEnemies();

        AIAction bestAction = selector.ChooseAction(context);
        if (bestAction != null)
        {
            bestAction.Execute(context);
        }
    }
    private List<BaseUnit> GetAllActiveAllies()
    {
        // Implement logic to retrieve all active allies.
    }

    private List<BaseUnit> GetAllActiveEnemies()
    {
        // Implement logic to retrieve all active enemies.
    }
}

