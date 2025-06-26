using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightRuler : MonoBehaviour
{
    [SerializeField] List<Light2D> lights;
    private void OnEnable()
    {
        foreach(var i in lights)
        {
            i.enabled = true;
        }
    }
    private void OnDisable()
    {
        foreach (var i in lights)
        {
            i.enabled = false;
        }
    }
}
