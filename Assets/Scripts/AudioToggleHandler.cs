using UnityEngine;
using UnityEngine.UI;

public class AudioToggleHandler : MonoBehaviour
{
    [Header("Which setting this toggle controls")]
    public string prefsKey = "MusicEnabled"; // e.g. "MusicEnabled", "SoundEnabled"

    [Header("UI")]
    public Toggle toggle;

    void Start()
    {
        // Load saved preference (default = ON if never set before)
        bool isOn = PlayerPrefs.GetInt(prefsKey, 1) == 1;
        toggle.isOn = isOn;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(prefsKey, isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (prefsKey == "MusicEnabled")
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicEnabled(isOn);
            }
        }
        else if (prefsKey == "SoundEnabled")
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSfxEnabled(isOn);
            }
        }
    }
}