using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text buttonLabel; // the TextMeshPro text INSIDE the button, e.g. "LEVEL 34"

    void Start()
    {
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        int currentLevel = LevelProgressManager.GetCurrentLevel();
        buttonLabel.text = "LEVEL " + currentLevel;
    }

    // Hook this up to the button's OnClick
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Gameplay");
    }
}