using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightState { Red, Yellow, Green }

    [Header("Current State")]
    public LightState currentState = LightState.Red;

    [Header("Visual Lights (assign the 3 circle sprites)")]
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    // Called by IntersectionController to change this light's state
    public void SetState(LightState newState)
    {
        currentState = newState;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        redLight.SetActive(currentState == LightState.Red);
        yellowLight.SetActive(currentState == LightState.Yellow);
        greenLight.SetActive(currentState == LightState.Green);
    }

    // Cars check this to decide whether to stop
    public bool IsStopRequired()
    {
        return currentState == LightState.Red || currentState == LightState.Yellow;
    }

    void Start()
    {
        UpdateVisuals();
    }
}