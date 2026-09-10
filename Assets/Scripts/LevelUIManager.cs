using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject failPanel;
    public GameObject winPanel;

    [Header("Win Panel Stars")]
    public GameObject[] starIcons; // drag Star1, Star2, Star3 GameObjects here, in order

    // Call this when the vehicle crashes
    public void ShowFailPanel()
    {
        failPanel.SetActive(true);
    }

    // Call this when the vehicle reaches the goal
    public void ShowWinPanel(int stars)
    {
        winPanel.SetActive(true);

        // Turn ON however many stars were earned, turn OFF the rest
        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(i < stars);
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