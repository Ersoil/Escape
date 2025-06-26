using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LineRenderer mainLine;
    [SerializeField] int reflections;
    [SerializeField] float maxRayDistance;
    [SerializeField] LayerMask layerDetection;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void OnDisable()
    {
        mainLine.enabled = false;
    }
    private void OnEnable()
    {
        mainLine.enabled = true;
    }

    private void Update()
    {
        mainLine.positionCount = 1;
        mainLine.SetPosition(0, transform.position);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxRayDistance, layerDetection);

        bool isMirror = false;

        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;
        for(int i =0; i < reflections; i++)
        {
            mainLine.positionCount += 1;
            if (hitInfo.collider != null)
            {
                mainLine.SetPosition(mainLine.positionCount - 1, hitInfo.point);
                isMirror = false;
                var LaserTrigg = hitInfo.collider.gameObject.GetComponent<Lasertrigger>();
                if (LaserTrigg != null)
                {
                    LaserTrigg.LaserEntered();
                }
                if (hitInfo.collider.CompareTag("Mirror"))
                {
                    mirrorHitPoint = (Vector2)hitInfo.point;
                    mirrorHitNormal = (Vector2)hitInfo.normal;
                    hitInfo = Physics2D.Raycast(mirrorHitPoint, Vector2.Reflect(mirrorHitPoint, mirrorHitNormal), maxRayDistance, layerDetection);
                    isMirror = true;
                }
                else
                    break;
            }
            else
            {
                if (isMirror)
                {
                    mainLine.SetPosition(mainLine.positionCount - 1, mirrorHitPoint + Vector2.Reflect(mirrorHitPoint, mirrorHitNormal) * maxRayDistance);
                    break;
                }
                else
                {
                    mainLine.SetPosition(mainLine.positionCount - 1, transform.position + transform.up * maxRayDistance);
                }
            }
        }
    }
}
