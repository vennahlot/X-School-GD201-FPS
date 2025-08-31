using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private float maxX = 40.0f;
    [SerializeField] private float minX = -40.0f;
    [SerializeField] private float maxZ = 40.0f;
    [SerializeField] private float minZ = -40.0f;
    [SerializeField] private float y = 2;
    [SerializeField] private float spawnInterval = 3.0f;
    [SerializeField] private List<GameObject> prefabsToSpawn;

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GameObject selectedSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
            Vector3 position = new(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));
            Instantiate(selectedSpawn, position, Quaternion.identity);
            spawnTimer = spawnInterval;
        }
    }
}
