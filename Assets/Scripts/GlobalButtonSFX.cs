using UnityEngine;
using UnityEngine.UI;

public class GlobalButtonSFX : MonoBehaviour
{
    void Start()
    {
        // Scene mein maujood SAARE Button components dhoondo (chhupe hue bhi
        // shamil, jaise Settings panel ke andar wale, jo shuru mein disabled hote hain)
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Button btn in allButtons)
        {
            // Har button ke click event mein hamara sound function add kar do
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayButtonClick();
        }
    }
}