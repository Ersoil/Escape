using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExtendedTrigger : MonoBehaviour
{
    public UnityEvent OnStay;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public string tag = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag==tag)
            OnEnter?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tag)
            OnExit?.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tag)
            OnStay?.Invoke();
    }
}
