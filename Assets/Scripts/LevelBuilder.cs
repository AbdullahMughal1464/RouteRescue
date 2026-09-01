using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Level Database")]
    public LevelDatabase levelDatabase; // drag your LevelDatabase asset here

    [Header("Prefabs")]
    public GameObject vehiclePrefab;
    public GameObject goalPrefab;
    public GameObject[] obstaclePrefabs; // index matches ObstacleData.spritePresetIndex

    private LevelData currentLevel;

    void Start()
    {
        int levelNumber = LevelProgressManager.GetCurrentLevel();
        currentLevel = levelDatabase.GetLevelByNumber(levelNumber);
        BuildLevel();
    }

    void BuildLevel()
    {
        if (currentLevel == null)
        {
            Debug.LogError("LevelBuilder: No LevelData assigned!");
            return;
        }

        // Spawn vehicle
        Instantiate(vehiclePrefab, currentLevel.vehicleStartPosition, Quaternion.identity);

        // Spawn goal
        Instantiate(goalPrefab, currentLevel.goalPosition, Quaternion.identity);

        // Spawn each obstacle
        foreach (ObstacleData obstacle in currentLevel.obstacles)
        {
            if (obstacle.spritePresetIndex < 0 || obstacle.spritePresetIndex >= obstaclePrefabs.Length)
            {
                Debug.LogWarning("Obstacle spritePresetIndex out of range, skipping.");
                continue;
            }

            GameObject prefabToUse = obstaclePrefabs[obstacle.spritePresetIndex];
            Quaternion rot = Quaternion.Euler(0, 0, obstacle.rotation);
            Instantiate(prefabToUse, obstacle.position, rot);
        }

        Debug.Log("Level built: " + currentLevel.levelName);
    }
}