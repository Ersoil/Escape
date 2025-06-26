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
        // Получаем ввод от игрока
        moveInput = Input.GetAxis("Horizontal");

        // Обработка прыжка
        if (Input.GetButtonDown("Jump"))
        {
            ballModel.Jump();
        }
    }

    private void FixedUpdate()
    {
        // Применяем движение в FixedUpdate для корректной физики
        ballModel.Move(moveInput);
    }
}
