using System.Collections.Generic;
using UnityEngine;

// Attach this to the Vehicle GameObject.
// Requires: Rigidbody2D, Collider2D on the same object.
[RequireComponent(typeof(Rigidbody2D))]
public class VehiclePathFollower : MonoBehaviour
{
    [Header("Drawing")]
    public LineRenderer pathLine;          // assign a LineRenderer (child object or same object)
    public float minPointDistance = 0.15f; // ignore points closer than this while drawing

    [Header("Movement")]
    public float moveSpeed = 4f;           // units per second along the path
    public float rotateSpeed = 10f;        // how fast the vehicle turns to face travel direction

    [HideInInspector] public float levelStartTime = -1f; // set the moment the first launch happens

    private List<Vector2> pathPoints = new List<Vector2>();
    private bool isDrawing = false;
    private bool isMoving = false;
    private int currentTargetIndex = 0;

    private Rigidbody2D rb;
    private Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (isMoving)
        {
            FollowPath();
            return; // don't allow new drawing while vehicle is moving
        }

        HandleDrawingInput();
    }

    void HandleDrawingInput()
    {
        // Works for both mouse (editor testing) and touch (mobile)
        bool inputDown = Input.GetMouseButtonDown(0);
        bool inputHeld = Input.GetMouseButton(0);
        bool inputUp = Input.GetMouseButtonUp(0);

        Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (inputDown)
        {
            float dist = Vector2.Distance(worldPos, transform.position);
            Debug.Log("Mouse down at " + worldPos + " | distance to vehicle: " + dist);

            // Only start drawing if the touch/click begins on the vehicle
            if (dist < 0.6f)
            {
                isDrawing = true;
                pathPoints.Clear();
                pathPoints.Add(transform.position);
                UpdateLineRenderer();
            }
        }
        else if (inputHeld && isDrawing)
        {
            Vector2 lastPoint = pathPoints[pathPoints.Count - 1];
            if (Vector2.Distance(worldPos, lastPoint) >= minPointDistance)
            {
                pathPoints.Add(worldPos);
                UpdateLineRenderer();
            }
        }
        else if (inputUp && isDrawing)
        {
            isDrawing = false;
            LaunchVehicle();
        }
    }

    void UpdateLineRenderer()
    {
        if (pathLine == null) return;
        pathLine.positionCount = pathPoints.Count;
        for (int i = 0; i < pathPoints.Count; i++)
        {
            pathLine.SetPosition(i, pathPoints[i]);
        }
    }

    void LaunchVehicle()
    {
        if (pathPoints.Count < 2) return; // ignore taps with no real path

        if (levelStartTime < 0f)
        {
            levelStartTime = Time.time; // record only the FIRST launch of this attempt
        }

        currentTargetIndex = 1; // index 0 is the starting position
        isMoving = true;
    }

    void FollowPath()
    {
        if (currentTargetIndex >= pathPoints.Count)
        {
            // Reached end of path
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
            pathPoints.Clear();
            UpdateLineRenderer();
            return;
        }

        Vector2 target = pathPoints[currentTargetIndex];
        Vector2 currentPos = rb.position;
        Vector2 direction = (target - currentPos).normalized;

        // Move
        Vector2 newPos = currentPos + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);

        // Rotate to face travel direction (optional, remove if using top-down square placeholder)
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle - 90f); // -90 if sprite faces up by default
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        // Check if close enough to current target point, advance to next
        if (Vector2.Distance(newPos, target) < 0.1f)
        {
            currentTargetIndex++;
        }
    }

    // Called by collision detection script (or directly here) on crash
    public void OnCrash()
    {
        isMoving = false;
        rb.linearVelocity = Vector2.zero;
        pathPoints.Clear();
        UpdateLineRenderer();
        Debug.Log("Vehicle crashed - reset level");
    }
}