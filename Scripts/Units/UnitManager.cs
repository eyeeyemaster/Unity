using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public List<TeamConfiguration> allTeams;  // Assign this via the Inspector.
    public Dictionary<TeamConfiguration, List<BaseUnit>> activeUnits = new Dictionary<TeamConfiguration, List<BaseUnit>>();

    public delegate void UnitEvent();
    public static event UnitEvent OnUnitSpawnedOrDestroyed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (TeamConfiguration teamConfig in allTeams)
        {
            activeUnits[teamConfig] = new List<BaseUnit>();
        }
    }

    public void RegisterUnit(BaseUnit unit, TeamConfiguration teamConfig)
    {
        activeUnits[teamConfig].Add(unit);
        OnUnitSpawnedOrDestroyed?.Invoke();
    }

    public void UnregisterUnit(BaseUnit unit, TeamConfiguration teamConfig)
    {
        activeUnits[teamConfig].Remove(unit);
        OnUnitSpawnedOrDestroyed?.Invoke();
    }
}
