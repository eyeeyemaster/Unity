using UnityEngine;
using System.Collections.Generic;

public class StealthArcherUnit : BaseUnit
{
    private bool isHidden = false;

    private List<StealthArcherUnit> allEnemies = new List<StealthArcherUnit>();  // Assume a manager updates this.

    protected override void Start()
    {
        base.Start();
        // Initialization related to stealth and vision.
        // Possibly register this unit with a manager that tracks all units, etc.
    }

    protected override void Update()
    {
        base.Update();
        if (IsOutOfEnemyVision())
        {
            TryToHide();
        }
    }

    public void Attack(BaseUnit target)
    {
        if (isHidden)
        {
            ApplyCriticalDamage(target);
        }
        else
        {
            ApplyStandardDamage(target);
        }
        isHidden = false;  // The unit reveals itself when attacking.
    }

    private void ApplyCriticalDamage(BaseUnit target)
    {
        target.TakeDamage(config.RangedAttack * config.CriticalMutiplier, DamageType.Physical);  // Example crit application, adjust as needed.
    }

    private void ApplyStandardDamage(BaseUnit target)
    {
        target.TakeDamage(config.RangedAttack, DamageType.Physical);  // Regular damage as defined in your unit's configuration.
    }

    private bool IsOutOfEnemyVision()
    {
        foreach (StealthArcherUnit enemy in allEnemies)
        {
            // Assumes a basic distance check for vision; refine as needed.
            if (Vector3.Distance(this.transform.position, enemy.transform.position) <= enemy.vision)
            {
                return false;
            }
        }
        return true;
    }

    private void TryToHide()
    {
        if (CanHide())
        {
            isHidden = true;
        }
    }

    private bool CanHide()
    {
        foreach (StealthArcherUnit enemy in allEnemies)
        {
            if (enemy.config.VisionRange > this.config.StealthValue)
            {
                return false;
            }
        }
        return true;
    }
}
