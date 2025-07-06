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
    private float _energyLifetime;

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
        _energyLifetime -= Time.deltaTime;
        _energyLifetime =  Mathf.Clamp(_energyLifetime, 0, 0.3f);

        Debug.Log(_energyLifetime);
        if (Input.GetButtonDown("Jump") || _energyLifetime <= 0)
        {
            this.enabled = false;
        }

    }
    public void AddEnergy()
    {
        _energyLifetime += 1;
    }

    private void FixedUpdate()
    {
        player.Rotate(moveInput);
    }
}
