using UnityEngine;

public class TeamSpawner : MonoBehaviour
{
    public GameObject creaturePrefab;
    public TeamConfiguration teamConfig;
    public float spawnInterval = 5f;

    private void Start()
    {
        InvokeRepeating("SpawnCreature", 0, spawnInterval);
    }

    void SpawnCreature()
    {
        GameObject spawnedCreature = Instantiate(creaturePrefab, transform.position, Quaternion.identity);

        // Assign the creature to the team layer.
        spawnedCreature.layer = teamConfig.layer;
    }
}
