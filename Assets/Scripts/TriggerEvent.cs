using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent Triggered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEvent");
        
        if(other.GetComponent<PlayerModel>()!=null) Triggered?.Invoke();
    }
}
