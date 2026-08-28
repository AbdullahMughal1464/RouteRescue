using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject failPanel;
    public GameObject winPanel;

    [Header("Win Panel Extras")]
    public TMP_Text starText; // drag a TextMeshPro text inside WinPanel here

    // Call this when the vehicle crashes
    public void ShowFailPanel()
    {
        failPanel.SetActive(true);
    }

    // Call this when the vehicle reaches the goal
    public void ShowWinPanel(int stars)
    {
        winPanel.SetActive(true);

        if (starText != null)
        {
            // Using plain characters here since not all fonts include the ★ unicode glyph.
            // We'll swap this for real star icon images later in the polish stage.
            string filled = new string('*', stars);
            string empty = new string('-', 3 - stars);
            starText.text = filled + empty;
        }
    }

    // Hook this up to the Retry button's OnClick
    public void OnRetryPressed()
    {
        // Reloads the current scene from scratch
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Hook this up to the Next Level button's OnClick
    // For now this just reloads the same scene too -
    // later you'll swap this for loading the next level's scene
    public void OnNextPressed()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}