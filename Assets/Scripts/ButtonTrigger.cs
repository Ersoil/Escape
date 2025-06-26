using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject TriggerCollider;
    public UnityEvent onButtonTiggerEntered;
    public void Invoke()
    {
        onButtonTiggerEntered?.Invoke();
    }
}
