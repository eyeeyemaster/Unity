using System.Collections.Generic;
using UnityEngine;

public class AIContext
{
    public BaseUnit selfUnit;
    public BaseUnit targetUnit;
    public List<BaseUnit> activeAllies = new List<BaseUnit>();
    public List<BaseUnit> activeEnemies = new List<BaseUnit>();
    public float detectionRadius;
    public LayerMask enemyLayers;

    public AIContext(BaseUnit unit)
    {
        this.selfUnit = unit;
    }
}

