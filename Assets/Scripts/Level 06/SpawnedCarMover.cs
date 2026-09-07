using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnedCarMover : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 moveDirection = Vector2.up; // which way this car drives
    public float moveSpeed = 3f;

    [Header("Traffic Light Check")]
    public TrafficLight controllingLight; // which light this lane obeys
    public float stopLineDistance = 1.5f; // how far before the light the car should stop
    public Vector2 stopLinePosition;       // world position of the stop line for this lane

    [Header("Despawn")]
    public Vector2 despawnPoint;    // position outside the scene where this car should disappear
    public float despawnDistance = 1f; // how close counts as "reached despawn point"

    private Rigidbody2D rb;
    private bool hasPassedStopLine = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Rotate car sprite to visually face its movement direction
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void FixedUpdate()
    {
        // Check if we're approaching the stop line and still need to stop
        float distanceToStopLine = Vector2.Distance(rb.position, stopLinePosition);

        bool shouldStop = !hasPassedStopLine
                           && distanceToStopLine < stopLineDistance
                           && controllingLight != null
                           && controllingLight.IsStopRequired();

        if (!shouldStop)
        {
            Vector2 newPos = rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);

            // Once we're past the stop line, never check the light again -
            // otherwise the car would awkwardly stop mid-intersection if light changes
            if (distanceToStopLine < 0.3f)
            {
                hasPassedStopLine = true;
            }
        }

        // Despawn once far enough along, outside the visible scene
        if (Vector2.Distance(rb.position, despawnPoint) < despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}