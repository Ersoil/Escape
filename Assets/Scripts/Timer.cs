using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{

    public float exitTime = 5f;
    public UnityEvent onExitTimer;

    void Update()
    {
        exitTime -= Time.deltaTime;
        if (exitTime < 0)
        {
            onExitTimer?.Invoke();
        }
    }
}
