using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LaserController : MonoBehaviour
{
    [SerializeField] Laser laser;
    [SerializeField] Rigidbody2D playerBody;
    [SerializeField] PlayerController playerControll;
    [SerializeField] PlayerModel player;
    [SerializeField] GameObject laserTriggerObject;
    [SerializeField] float timeout;
    InputSystem_Actions Actions;
    private float moveInput;
    private float _energyLifetime;
    private int isDisabling = 0;

    private void Awake()
    {
        Actions = new InputSystem_Actions();
        Actions.Enable();
        Actions.PlayerControl.Jump.performed += context => DisableControl();
    }
    private void OnEnable()
    {
        laser.enabled = true;
        playerBody.angularVelocity = 0;
        playerBody.velocity = Vector3.zero;
        playerBody.isKinematic = true;
        playerControll.enabled = false;
        isDisabling = 0;
    }

    private void OnDisable()
    {
        laser.enabled = false;
        playerBody.isKinematic = false;
        playerControll.enabled = true;
        isDisabling = 0;
    }

    private void Update()
    {
        moveInput = Actions.PlayerControl.Move.ReadValue<float>();
        _energyLifetime -= Time.deltaTime;
        _energyLifetime =  Mathf.Clamp(_energyLifetime, 0, 0.3f);

        Debug.Log($"Energy:{_energyLifetime}");
        if (isDisabling == 1 || _energyLifetime <= 0)
        {
            this.enabled = false;
            laserTriggerObject.GetComponent<Lasertrigger>().setTimer(timeout);
        }

    }
    public void AddEnergy()
    {
        _energyLifetime += 1;
    }
    private void DisableControl()
    {
        isDisabling = 1;
    }
    private void FixedUpdate()
    {
        player.Rotate(moveInput);
    }
}
