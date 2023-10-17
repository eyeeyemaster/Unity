using UnityEngine;
using UnityEngine.AI;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject[] creaturePrefabs;  // Drag your creature prefabs here.
    public Transform target;              // The target they should walk towards.
    public float spawnInterval = 5f;      // Time between spawns.
    public TeamConfiguration teamConfig;

    private void Start()
    {
        InvokeRepeating("SpawnCreature", 0, spawnInterval);
    }

    void SpawnCreature()
    {
        // Select a random creature prefab.
        GameObject randomCreaturePrefab = creaturePrefabs[Random.Range(0, creaturePrefabs.Length)];

        // Instantiate the creature and retrieve the NavMeshAgent component.
        GameObject creature = Instantiate(randomCreaturePrefab, transform.position, Quaternion.identity);
        NavMeshAgent agent = creature.GetComponent<NavMeshAgent>();
        BaseUnit baseUnit = creature.GetComponent<BaseUnit>();
        if (baseUnit != null)
        {
            baseUnit.SetTeamConfiguration(teamConfig);
            creature.layer = teamConfig.layer;
        }
        // Set the destination if applicable.
        if (agent != null && target != null)
        {
            agent.destination = target.position;
        }
    }
}
