using UnityEngine;

// Attach this to any obstacle car that should move instead of sitting still.
// It moves smoothly back and forth between a start point and an end point.
[RequireComponent(typeof(Rigidbody2D))]
public class MovingObstacle : MonoBehaviour
{
    [Header("Movement Path")]
    public Vector2 pointA;      // one end of the movement path
    public Vector2 pointB;      // other end of the movement path
    public float moveSpeed = 2f;

    [Header("Behavior")]
    public bool startAtPointA = true;
    public bool rotateToFaceMovement = true;

    private Vector2 currentTarget;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // moves manually, not affected by physics forces

        // Start at whichever point was chosen, and head toward the other one
        transform.position = startAtPointA ? (Vector3)pointA : (Vector3)pointB;
        currentTarget = startAtPointA ? pointB : pointA;
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 direction = (currentTarget - currentPos).normalized;

        Vector2 newPos = currentPos + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        if (rotateToFaceMovement && direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.MoveRotation(angle - 90f); // -90 assumes sprite faces "up" by default, same as Vehicle
        }

        // If we've basically reached the target, flip direction to the other point
        if (Vector2.Distance(newPos, currentTarget) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    // Helpful visual guide in the Scene view (Editor only) so you can see
    // the movement path while designing a level, without needing Play mode.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA, pointB);
        Gizmos.DrawWireSphere(pointA, 0.2f);
        Gizmos.DrawWireSphere(pointB, 0.2f);
    }
}