using UnityEngine;

// This creates a reusable "template" for level data.
// Each individual level becomes an .asset file based on this template,
// instead of a whole separate Scene.
[CreateAssetMenu(fileName = "Level_", menuName = "RouteRescue/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Identification")]
    public string levelName = "Level 1";
    public int levelNumber = 1;

    [Header("Vehicle")]
    public Vector2 vehicleStartPosition;

    [Header("Goal")]
    public Vector2 goalPosition;

    [Header("Obstacles")]
    public ObstacleData[] obstacles;

    [Header("Star Rating")]
    public float threeStarTime = 5f;
    public float twoStarTime = 8f;
}

// A small data container describing one obstacle's placement.
// Marked [System.Serializable] so it shows up nicely in the Inspector
// as part of the array above, without needing its own ScriptableObject.
[System.Serializable]
public class ObstacleData
{
    public Vector2 position;
    public float rotation;
    public int spritePresetIndex; // which car sprite to use, see Step 3
}