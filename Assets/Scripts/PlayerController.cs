using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel ballModel;
    private float moveInput;

    private void Awake()
    {
        ballModel = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            ballModel.Jump();
        }
    }

    private void FixedUpdate()
    {
        ballModel.Move(moveInput);
    }
}
