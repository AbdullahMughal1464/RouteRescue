using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this from any button's OnClick, passing the exact scene name as a string.
    // Example: LoadScene("LevelSelect") or LoadScene("Level_01")
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}