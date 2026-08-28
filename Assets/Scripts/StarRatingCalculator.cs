using UnityEngine;

public class StarRatingCalculator : MonoBehaviour
{
    [Header("Time thresholds (seconds)")]
    [Tooltip("Finish at or under this time = 3 stars")]
    public float threeStarTime = 5f;

    [Tooltip("Finish at or under this time = 2 stars")]
    public float twoStarTime = 8f;

    // Anything slower than twoStarTime = 1 star (as long as you finished at all)

    public int CalculateStars(float startTime)
    {
        if (startTime < 0f)
        {
            // Player never actually launched the vehicle - shouldn't normally happen
            // if this is only called on a win, but guard anyway
            return 1;
        }

        float elapsed = Time.time - startTime;

        if (elapsed <= threeStarTime) return 3;
        if (elapsed <= twoStarTime) return 2;
        return 1;
    }
}