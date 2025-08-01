using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameras : MonoBehaviour
{
    [SerializeField] Camera worldCamera;
    private int count = 1;

    private void Update()
    {
        if (count != 0)
        {
            FindObjectOfType<Canvas>().worldCamera = worldCamera;
            this.enabled = false;
            count--;
        }
        
    }
}
