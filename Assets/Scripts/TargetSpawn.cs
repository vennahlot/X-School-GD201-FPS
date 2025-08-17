using System.Collections;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float spawnInterval = 5.0f;
    [SerializeField] private int maxTargets = 10;
    [SerializeField] private Transform playerTransform;
    private int currentTargets = 0;

    public static TargetSpawn Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnTargetRoutine());
    }

    IEnumerator SpawnTargetRoutine()
    {
        while (true)
        {
            if (currentTargets < maxTargets)
            {
                GameObject targetInstance = Instantiate(
                    targetPrefab,
                    new Vector3(
                        Random.Range(-10, 10),
                        Random.Range(2, 4),
                        Random.Range(-10, 10)
                    ),
                    Quaternion.identity
                );
                targetInstance.GetComponent<TargetController>().playerTransform = playerTransform;
                currentTargets += 1;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TargetDestroyed()
    {
        currentTargets -= 1;
    }

}