using System;
using UnityEngine;
using UnityEngine.Events;

public class RaycastEvent : MonoBehaviour
{
    [Tooltip("Length max of the raycast")]
    public float detectionRange = 3;
    [Tooltip("The layer who trigger the event")]
    public LayerMask layer;
    public Transform m_rayStartPoint;
    public DistanceChangeEvent m_onDistanceChange;
    [Header("Debug")]
    [SerializeField] bool hasHitDebug;
    [SerializeField] float currentRatio;
     float previousRatio;
    public RaycastHit raycastHit;

    private void Update()
    {
        CheckForCollisionWithRaycast(out hasHitDebug, out currentRatio);
        TriggerEventIfRatioChangd();
        previousRatio = currentRatio;
    }

    private void TriggerEventIfRatioChangd()
    {
        if (currentRatio != previousRatio) {
            m_onDistanceChange.Invoke(currentRatio);
        }
    }

    private void CheckForCollisionWithRaycast(out bool hasHitSomething, out float ratioFound)     
    {
        bool rayHit = Physics.Raycast(m_rayStartPoint.position, m_rayStartPoint.forward, out raycastHit, detectionRange, layer);
        if (rayHit && raycastHit.distance <= detectionRange)
        {
            hasHitSomething = true;
            ratioFound = (1f - raycastHit.distance / detectionRange); 
        }
        else
        {
            hasHitSomething = false;
            ratioFound = 0f;
        }

    }

    private void OnDrawGizmos()
    {
        Vector3 targetPos = new Vector3(m_rayStartPoint.position.x, m_rayStartPoint.position.y, m_rayStartPoint.position.z + detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);

    }

    private void Reset()
    {
        m_rayStartPoint = transform;
    }

    [System.Serializable]
    public class DistanceChangeEvent : UnityEvent<float> { }
}
