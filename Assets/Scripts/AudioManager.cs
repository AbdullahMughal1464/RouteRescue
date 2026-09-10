using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // globally accessible from any script

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    [Header("Sound Effects")]
    public AudioSource sfxSource;
    public AudioClip buttonClickSound;

    private bool sfxEnabled = true;

    void Awake()
    {
        // Singleton pattern: agar pehle se ek AudioManager maujood hai (kisi purani
        // scene se), to ye naya wala khud ko destroy kar de - taake music dobara
        // se shuru na ho har baar scene change hone par
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // is object ko scene change par bhi zinda rakhta hai
    }

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;

        bool musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        musicSource.mute = !musicEnabled;

        sfxEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;

        musicSource.Play();
    }

    // Music toggle button isay call karega
    public void SetMusicEnabled(bool isOn)
    {
        musicSource.mute = !isOn;
    }

    // Sound toggle button isay call karega
    public void SetSfxEnabled(bool isOn)
    {
        sfxEnabled = isOn;
    }

    // Har button press par ye call hoga
    public void PlayButtonClick()
    {
        if (sfxEnabled && buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }
}