using UnityEngine;

public class TrafficCarSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] carPrefabs;      // different car sprites for variety
    public Vector2 spawnPoint;           // position outside the scene where cars appear
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 4f;

    [Header("Lane Settings (passed to each spawned car)")]
    public Vector2 moveDirection = Vector2.up;
    public float moveSpeed = 3f;
    public TrafficLight controllingLight;
    public Vector2 stopLinePosition;
    public float stopLineDistance = 1.5f;
    public Vector2 despawnPoint;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(wait);
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        GameObject prefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject car = Instantiate(prefab, spawnPoint, Quaternion.identity);

        SpawnedCarMover mover = car.GetComponent<SpawnedCarMover>();
        if (mover == null)
        {
            mover = car.AddComponent<SpawnedCarMover>();
        }

        mover.moveDirection = moveDirection;
        mover.moveSpeed = moveSpeed;
        mover.controllingLight = controllingLight;
        mover.stopLinePosition = stopLinePosition;
        mover.stopLineDistance = stopLineDistance;
        mover.despawnPoint = despawnPoint;
    }
}
