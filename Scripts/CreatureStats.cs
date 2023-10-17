using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureStats
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Vector2 damageRange = new Vector2(10f, 20f); // Damage can range from 10 to 20, for instance.

    // Constructor
    public CreatureStats()
    {
        currentHealth = maxHealth;
    }

    public float DealDamage()
    {
        return Random.Range(damageRange.x, damageRange.y);
    }
}

