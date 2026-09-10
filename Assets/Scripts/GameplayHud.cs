using UnityEngine;
using TMPro;

// Ye ek hi script ab teen kaam karta hai jo pehle 3 alag scripts mein the:
// 1. Top header mein "Level 0X" dikhana
// 2. Timer count karna
// 3. Settings panel open/close karna
public class GameplayHUD : MonoBehaviour
{
    [Header("Level Header")]
    public TMP_Text levelHeaderText;

    [Header("Timer")]
    //public TMP_Text timerText;
    private float elapsedTime = 0f;
    private bool timerRunning = true;

    [Header("Settings Panel")]
    public GameObject settingsOverlay;

    void Start()
    {
        // Header set karna
        LevelInfo info = FindFirstObjectByType<LevelInfo>();
        levelHeaderText.text = info != null ? "Level " + info.levelNumber.ToString("00") : "Level --";
    }

    void Update()
    {
        if (!timerRunning) return;

        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
     //   timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void OpenSettings()
    {
        settingsOverlay.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsOverlay.SetActive(false);
    }
}