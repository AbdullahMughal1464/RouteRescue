using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VehicleCollisionHandler : MonoBehaviour
{
    [Header("UI")]
    public LevelUIManager uiManager; // drag the Canvas object here in the Inspector

    [Header("Scoring")]
    public StarRatingCalculator starCalculator; // drag any object with this script attached

    private VehiclePathFollower pathFollower;

    void Awake()
    {
        pathFollower = GetComponent<VehiclePathFollower>();
    }

    // Called automatically when this object's non-trigger collider
    // physically hits another non-trigger collider (like Obstacle)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            HandleCrash();
        }
    }

    // Called automatically when this object enters a collider
    // marked "Is Trigger" (like our Goal)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            HandleWin();
        }
    }

    void HandleCrash()
    {
        Debug.Log("CRASH! Level failed.");
        pathFollower.OnCrash(); // stops movement, clears the drawn path

        if (uiManager != null)
        {
            uiManager.ShowFailPanel();
        }
    }

    void HandleWin()
    {
        Debug.Log("WIN! Level complete.");
        pathFollower.OnCrash(); // reuse this to stop the vehicle cleanly

        LevelProgressManager.AdvanceToNextLevel(); // player moves on to the next level next time

        int stars = 1;
        if (starCalculator != null)
        {
            stars = starCalculator.CalculateStars(pathFollower.levelStartTime);
        }

        if (uiManager != null)
        {
            uiManager.ShowWinPanel(stars);
        }
    }
}