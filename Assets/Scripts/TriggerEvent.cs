using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public string Tag;
    public UnityEvent Triggered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag))
        {
            Debug.Log("TriggerEvent");
            Triggered?.Invoke();
        }
    }
}
