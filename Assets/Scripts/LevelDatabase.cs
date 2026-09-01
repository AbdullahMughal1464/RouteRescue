using UnityEngine;

// One single asset that holds every level in order.
// LevelBuilder will look up "give me level number 34" from this list.
[CreateAssetMenu(fileName = "LevelDatabase", menuName = "RouteRescue/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public LevelData[] levels; // index 0 = Level 1, index 1 = Level 2, etc.

    public LevelData GetLevelByNumber(int levelNumber)
    {
        int index = levelNumber - 1; // levels are 1-based for players, arrays are 0-based

        if (index < 0 || index >= levels.Length)
        {
            Debug.LogWarning("Requested level " + levelNumber + " does not exist. Returning last available level.");
            index = Mathf.Clamp(index, 0, levels.Length - 1);
        }

        return levels[index];
    }

    public int TotalLevelCount()
    {
        return levels.Length;
    }
}
