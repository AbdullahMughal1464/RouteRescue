using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitchVisual : MonoBehaviour
{
    [Header("References")]
    public Toggle toggle;           // the Toggle component itself
    public Image switchImage;       // the "SwitchVisual" background image

    [Header("Sprites")]
    public Sprite onSprite;   // full switch graphic showing ON (green, knob right)
    public Sprite offSprite;  // full switch graphic showing OFF (red, knob left)

    void Start()
    {
        // Set initial visual to match whatever state the toggle starts in
        UpdateVisual(toggle.isOn);

        // Whenever the toggle's value changes (user taps it), update the sprite
        toggle.onValueChanged.AddListener(UpdateVisual);
    }

    void UpdateVisual(bool isOn)
    {
        switchImage.sprite = isOn ? onSprite : offSprite;
    }
}