using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private int count = 1;
    public float exitTime = 5f;
    public UnityEvent onExitTimer;

    void Update()
    {
        exitTime -= Time.deltaTime;
        if (exitTime < 0 && count>0)
        {
            onExitTimer?.Invoke();
            count--;
        }
    }
    public void newTime(float time)
    {
        exitTime = time;
        count++;
    }
}
