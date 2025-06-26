using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] GameObject EventBusObject;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Destroyer>()!=null)
        {
            EventBusObject.GetComponent<EventBus>().onPlayerDeath?.Invoke();
        }
    }
}
