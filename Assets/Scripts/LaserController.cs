using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] Laser laser;
    [SerializeField] Rigidbody2D playerBody;
    [SerializeField] PlayerController playerControll;
    [SerializeField] PlayerModel player;
    private float moveInput;

    private void OnEnable()
    {
        laser.enabled = true;
        playerBody.angularVelocity = 0;
        playerBody.velocity = Vector3.zero;
        playerBody.isKinematic = true;
        playerControll.enabled = false;
        
    }

    private void OnDisable()
    {
        laser.enabled = false;
        playerBody.isKinematic = false;
        playerControll.enabled = true;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            this.enabled = false;
        }

    }

    private void FixedUpdate()
    {
        player.Rotate(moveInput);
    }
}
