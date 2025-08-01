using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private Vector2 followSpeeds = new Vector2(5f, 3f);

    [Header("Look Ahead Settings")]
    [SerializeField] private bool lookAhead = true; 
    [SerializeField] private float lookAheadDistance = 2f;
    [SerializeField] private float lookAheadSpeed = 1f;

    [Header("Camera Bounds")]
    [SerializeField] private bool useBounds = false;
    [SerializeField] private GameObject minBounds;
    [SerializeField] private GameObject maxBounds;

    private Vector3 targetPosition;
    private Vector3 lookAheadOffset;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D targetRb;
    private bool isSetingField = false;
    private float setField;

    private void Start()
    {


        if (target != null)
        {
            targetRb = target.GetComponent<Rigidbody2D>();
        }

        if (target != null)
        {
            transform.position = new Vector3(
                target.position.x,
                target.position.y,
                transform.position.z
            );
        }
    }
    public void setTarget(GameObject target)
    {
        targetRb = target.GetComponent<Rigidbody2D>();
        this.target = target.transform;
    }
    private void LateUpdate()
    {
        
        if (isSetingField)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, setField, Time.deltaTime * 0.5f);
        }

        if (target == null) return;
        if (Time.timeScale == 0f) return;

        targetPosition = target.position;
        targetPosition.z = transform.position.z; 

        if (lookAhead && targetRb != null)
        {
            Vector2 currentVelocity = targetRb.velocity;

            if (currentVelocity.magnitude > 0.1f)
            {
                Vector2 targetLookAhead = currentVelocity.normalized * lookAheadDistance;
                lookAheadOffset = Vector3.Lerp(
                    lookAheadOffset,
                    targetLookAhead,
                    lookAheadSpeed * Time.deltaTime
                );
            }
            else
            {
                lookAheadOffset = Vector3.Lerp(
                    lookAheadOffset,
                    Vector3.zero,
                    lookAheadSpeed * Time.deltaTime
                );
            }

            targetPosition += lookAheadOffset;
        }

        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.transform.position.x, maxBounds.transform.position.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.transform.position.y, maxBounds.transform.position.y);
        }

        float posX = Mathf.SmoothDamp(
            transform.position.x,
            targetPosition.x,
            ref velocity.x,
            0
        );

        float posY = Mathf.SmoothDamp(
            transform.position.y,
            targetPosition.y,
            ref velocity.y,
            0
        );

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    public void DisableBounds()
    {
        useBounds = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (useBounds)
        {
            Gizmos.color = Color.green;
            Vector3 center = new Vector3(
                (maxBounds.transform.position.x + minBounds.transform.position.x) * 0.5f,
                (maxBounds.transform.position.y + minBounds.transform.position.y) * 0.5f,
                0
            );
            Vector3 size = new Vector3(
                maxBounds.transform.position.x - minBounds.transform.position.x,
                maxBounds.transform.position.y - minBounds.transform.position.y,
                1
            );
            Gizmos.DrawWireCube(center, size);
        }
    }

    public void setCameraFieldOfView(float field)
    {
        isSetingField = true;
        setField = field;
    }
}
