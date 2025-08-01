using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lasertrigger : MonoBehaviour
{
    public int countUse = 1;
    private int currentUse = 0;
    private float Timer = 0;
    public bool everlasting = false;
    private bool inLaser = false;

    public UnityEvent OnLaserEntered;

    public void LaserEntered()
    {
        inLaser = true;
        if ((currentUse <= countUse || everlasting) && Timer <=0)
        {
            OnLaserEntered?.Invoke();
            currentUse++;
        }
    }
    public void setTimer(float timeout)
    {
        Timer = timeout;
    }
    private void Update()
    {
        Timer -= Time.deltaTime;
        if(inLaser==false && Timer <=0)
        {
            currentUse = 0;
            Timer = 0;
        }
        inLaser = false;
    }
}
