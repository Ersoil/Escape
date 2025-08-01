using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerModel ballModel;
    InputSystem_Actions Actions;
    private float moveInput;

    private void Awake()
    {
        Actions = new InputSystem_Actions();
        Actions.Enable();
        ballModel = GetComponent<PlayerModel>();
        Actions.PlayerControl.Jump.performed += context => ballModel.Jump();
    }

    private void Update()
    {
        moveInput = Actions.PlayerControl.Move.ReadValue<float>();
        Debug.Log(moveInput);
    }

    private void FixedUpdate()
    {
        ballModel.Move(moveInput);
    }
}
