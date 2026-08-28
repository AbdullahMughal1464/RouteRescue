using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VehiclePathFollower : MonoBehaviour
{
    [Header("Drawing")]
    public LineRenderer pathLine;          // assign the LineRenderer you just added
    public float minPointDistance = 0.15f; // ignore points closer than this while drawing

    [Header("Movement")]
    public float moveSpeed = 4f;           // units per second along the path
    public float rotateSpeed = 10f;        // how fast the vehicle turns to face travel direction

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
        bool inputDown = Input.GetMouseButtonDown(0);
        bool inputHeld = Input.GetMouseButton(0);
        bool inputUp = Input.GetMouseButtonUp(0);

        Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (inputDown)
        {
            if (Vector2.Distance(worldPos, transform.position) < 0.6f)
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
        if (pathPoints.Count < 2) return;
        currentTargetIndex = 1; // index 0 is the starting position
        isMoving = true;
    }

    void FollowPath()
    {
        if (currentTargetIndex >= pathPoints.Count)
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
            pathPoints.Clear();
            UpdateLineRenderer();
            return;
        }

        Vector2 target = pathPoints[currentTargetIndex];
        Vector2 currentPos = rb.position;
        Vector2 direction = (target - currentPos).normalized;

        Vector2 newPos = currentPos + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle - 90f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(newPos, target) < 0.1f)
        {
            currentTargetIndex++;
        }
    }

    public void OnCrash()
    {
        isMoving = false;
        rb.linearVelocity = Vector2.zero;
        pathPoints.Clear();
        UpdateLineRenderer();
        Debug.Log("Vehicle crashed - reset level");
    }
}