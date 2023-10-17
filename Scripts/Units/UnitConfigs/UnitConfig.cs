using UnityEngine;

[CreateAssetMenu(menuName = "Unit Configuration")]
public class UnitConfig : ScriptableObject
{
    public string unitName;
    public GameObject visualRepresentation;

    // Base stats
    [SerializeField] private float endurance; //implemented
    [SerializeField] private float strength; //implemented
    [SerializeField] private float criticalChance; //implemented
    [SerializeField] private float criticalMultiplier; //implemented
    [SerializeField] private float stealth; 
    [SerializeField] private float rangedDamage;
    [SerializeField] private float range; //implemented
    [SerializeField] private float movementSpeed; //implemented
    [SerializeField] private float attackSpeed;
    [SerializeField] private float regeneration;
    [SerializeField] private float regenerationSpeed;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegen;
    [SerializeField] private float buffDuration;
    [SerializeField] private float debuffDuration;
    [SerializeField] protected bool isMeleeUnit;


    // Resistances
    [SerializeField] private float lightningResist;
    [SerializeField] private float fireResist;
    [SerializeField] private float iceResist;
    [SerializeField] private float physicalResist;

    // Calculated resistances with percentages
    public float LightningResistPercentage => CalculateResistancePercentage(lightningResist);
    public float FireResistPercentage => CalculateResistancePercentage(fireResist);
    public float IceResistPercentage => CalculateResistancePercentage(iceResist);
    public float PhysicalResistPercentage => CalculateResistancePercentage(physicalResist);

    private float CalculateResistancePercentage(float resistanceValue)
    {
        float calculatedValue = resistanceValue * (resistanceValue + 1) / 2f;
        return calculatedValue / (calculatedValue + 100);
    }

    // Vision
    public float vision;

    // Derived attributes
    public float maxHealth => (CalculateAttribute(endurance) +1)*5;
    public float MeleeDamage => CalculateAttribute(strength) +1;
    public float CriticalChance => CalculateResistancePercentage(criticalChance) * 100;
    public float CriticalMutiplier => 2;
    public float StealthValue => CalculateAttribute(stealth);
    public float RangedAttack => CalculateAttribute(rangedDamage);
    public float VisionRange => CalculateAttribute(vision) + 10;
    public float TrueRange => range + 2f;
    public float TrueSpeed => movementSpeed + 2f;
    public float AttackSpeed => 200f / (100f + CalculateAttribute(attackSpeed));
    public float Regeneration => CalculateAttribute(regeneration);
    public float RegenerationSpeed => 200f / (100f + CalculateAttribute(regenerationSpeed));

    private float CalculateAttribute(float statValue)
    {
        return statValue * (statValue + 1) / 2f;
    }
}
