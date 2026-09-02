using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    [Header("Level Info")]
    public int levelNumber = 1;
    public string sceneName = "Level_01"; // exact scene name to load

    [Header("UI References")]
    public TMP_Text numberText;         // shows "1", "2", "3" etc.
    public GameObject[] starIcons;      // 3 star image objects, left to right
    public GameObject lockedOverlay;    // shown when level is locked
    public Button button;               // this button's own Button component

    void Start()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        numberText.text = levelNumber.ToString();

        bool unlocked = LevelSaveManager.IsUnlocked(levelNumber);
        int stars = LevelSaveManager.GetStars(levelNumber);

        // Locked levels show a lock icon and can't be clicked
        lockedOverlay.SetActive(!unlocked);
        button.interactable = unlocked;

        // Turn on however many star icons match the saved score
        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(i < stars);
        }
    }

    // Hook this up to the Button's OnClick
    public void OnLevelButtonPressed()
    {
        SceneManager.LoadScene(sceneName);
    }
}