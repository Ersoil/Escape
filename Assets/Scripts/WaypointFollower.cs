using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public enum MovementType
    {
        Loop,       
        PingPong,   
        Once     
    }

    [Header("Waypoints")]
    public List<Vector2> waypoints = new List<Vector2>();
    public MovementType movementType = MovementType.Loop;
    public float movementSpeed = 3f;
    public float reachThreshold = 0.1f;

    [Header("Rotation Settings")]
    public bool rotateToDirection = true;
    public float rotationSpeed = 5f;

    private int currentWaypointIndex = 0;
    private bool movingForward = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (waypoints.Count == 0)
        {
            waypoints.Add(transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (waypoints.Count <= 1) return;

        Vector2 targetPosition = waypoints[currentWaypointIndex];
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

        rb.velocity = moveDirection * movementSpeed;

        if (rotateToDirection && rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, targetPosition) < reachThreshold)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        switch (movementType)
        {
            case MovementType.Loop:
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                break;

            case MovementType.PingPong:
                if (movingForward)
                {
                    if (currentWaypointIndex >= waypoints.Count - 1)
                    {
                        movingForward = false;
                        currentWaypointIndex--;
                    }
                    else
                    {
                        currentWaypointIndex++;
                    }
                }
                else
                {
                    if (currentWaypointIndex <= 0)
                    {
                        movingForward = true;
                        currentWaypointIndex++;
                    }
                    else
                    {
                        currentWaypointIndex--;
                    }
                }
                break;

            case MovementType.Once:
                if (currentWaypointIndex < waypoints.Count - 1)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    enabled = false; 
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints.Count > 0)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.DrawSphere(waypoints[i], 0.2f);
                if (i < waypoints.Count - 1)
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
                }
                else if (movementType == MovementType.Loop)
                {
                    Gizmos.DrawLine(waypoints[i], waypoints[0]);
                }
            }
        }
    }
}
