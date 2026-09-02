using UnityEngine;
using TMPro;

public class TotalStarsDisplay : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text totalStarsText; // shows "45/90"

    [Header("Settings")]
    public int totalLevelCount = 20; // update this as you add more levels

    void Start()
    {
        int earned = LevelSaveManager.GetTotalStars(totalLevelCount);
        int possible = totalLevelCount * 3; // max 3 stars per level

        totalStarsText.text = earned + "/" + possible;
    }
}