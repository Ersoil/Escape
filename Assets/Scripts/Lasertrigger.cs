using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lasertrigger : MonoBehaviour
{
    public int countUse = 1;
    private int currentUse = 0;
    private float Timer = 0;
    private bool inLaser = false;

    public UnityEvent OnLaserEntered;

    public void LaserEntered()
    {
        inLaser = true;
        if (currentUse <= countUse)
        {
            OnLaserEntered?.Invoke();
            currentUse++;
        }
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        Timer = Timer % 2;
        if(inLaser==false && Timer < 1)
        {
            currentUse = 0;
        }
        inLaser = false;
    }
}
