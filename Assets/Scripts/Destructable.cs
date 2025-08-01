using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{
    public UnityEvent OnDestroy;
    private int count = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Destroyer>()!=null && count == 0)
        {
            count++;
            OnDestroy?.Invoke();
        }
    }
}
