using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxSpeed = 5f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Collider2D ballCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

    public void Move(float direction)
    {
        if (Mathf.Abs(direction) > 0.1f)
        {
            rb.AddForce(new Vector2(direction * moveForce, 0), ForceMode2D.Impulse);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public void Rotate(float Direction)
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime*Direction);
    }
}
