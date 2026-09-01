using UnityEngine;

// Static class = accessible from anywhere without needing a reference.
// Handles saving/loading which level the player is currently on.
public static class LevelProgressManager
{
    private const string CURRENT_LEVEL_KEY = "CurrentLevel";

    // Returns the level number the player should play next.
    // Defaults to 1 if this is a brand new player with no saved data.
    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);
    }

    // Call this when a level is completed to advance progress.
    public static void AdvanceToNextLevel()
    {
        int current = GetCurrentLevel();
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, current + 1);
        PlayerPrefs.Save();
    }

    // Optional: useful during testing to reset progress back to Level 1.
    public static void ResetProgress()
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, 1);
        PlayerPrefs.Save();
    }
}