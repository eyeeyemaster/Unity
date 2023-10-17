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
    public bool retreating; // Added retreating field
    public float range; // Added range field
    public float recentDamage; // Added recentDamage field
    public Vector3 spawnPoint; // Added spawnPoint field

    public AIContext(BaseUnit unit)
    {
        this.selfUnit = unit;
    }
}

