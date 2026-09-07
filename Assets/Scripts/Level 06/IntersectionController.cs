using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    [Header("The two opposing traffic lights")]
    public TrafficLight lightA; // e.g. horizontal traffic
    public TrafficLight lightB; // e.g. vertical traffic

    [Header("Timing (seconds)")]
    public float greenDuration = 4f;
    public float yellowDuration = 1f;

    void Start()
    {
        StartCoroutine(RunCycle());
    }

    System.Collections.IEnumerator RunCycle()
    {
        while (true)
        {
            // Phase 1: A is green, B is red
            lightA.SetState(TrafficLight.LightState.Green);
            lightB.SetState(TrafficLight.LightState.Red);
            yield return new WaitForSeconds(greenDuration);

            // Phase 2: A turns yellow (warning before red)
            lightA.SetState(TrafficLight.LightState.Yellow);
            yield return new WaitForSeconds(yellowDuration);

            // Phase 3: A is red, B is green
            lightA.SetState(TrafficLight.LightState.Red);
            lightB.SetState(TrafficLight.LightState.Green);
            yield return new WaitForSeconds(greenDuration);

            // Phase 4: B turns yellow before switching back
            lightB.SetState(TrafficLight.LightState.Yellow);
            yield return new WaitForSeconds(yellowDuration);
        }
    }
}
