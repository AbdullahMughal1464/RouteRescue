using UnityEngine;

// Static class = kahin se bhi call ho sakti hai bina reference ke.
// Har level ka star count aur unlock status PlayerPrefs mein save karti hai.
public static class LevelSaveManager
{
    // Kitne stars is level par mile (0 = kabhi khela nahi, 1-3 = best score)
    public static int GetStars(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level_" + levelNumber + "_Stars", 0);
    }

    // Level complete hone par call karein - agar naya score purane se behtar hai to save karein
    public static void SetStars(int levelNumber, int stars)
    {
        int existing = GetStars(levelNumber);
        if (stars > existing)
        {
            PlayerPrefs.SetInt("Level_" + levelNumber + "_Stars", stars);
        }

        // Agla level automatically unlock kar dein
        UnlockLevel(levelNumber + 1);

        PlayerPrefs.Save();
    }

    // Kya ye level khelne layak hai (unlocked) ya lock hai
    public static bool IsUnlocked(int levelNumber)
    {
        // Level 1 hamesha unlocked hota hai
        if (levelNumber == 1) return true;

        return PlayerPrefs.GetInt("Level_" + levelNumber + "_Unlocked", 0) == 1;
    }

    public static void UnlockLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("Level_" + levelNumber + "_Unlocked", 1);
        PlayerPrefs.Save();
    }

    // Sab levels ke stars jama kar ke total deta hai (top counter ke liye)
    public static int GetTotalStars(int totalLevelCount)
    {
        int total = 0;
        for (int i = 1; i <= totalLevelCount; i++)
        {
            total += GetStars(i);
        }
        return total;
    }

    // Testing ke liye - sab progress reset kar deta hai
    public static void ResetAllProgress(int totalLevelCount)
    {
        for (int i = 1; i <= totalLevelCount; i++)
        {
            PlayerPrefs.DeleteKey("Level_" + i + "_Stars");
            PlayerPrefs.DeleteKey("Level_" + i + "_Unlocked");
        }
        PlayerPrefs.Save();
    }
}