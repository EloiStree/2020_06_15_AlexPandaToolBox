using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeadPoximityWithObjectsSphereChecker : MonoBehaviour
{
    public LayerMask m_collisionChecker;
    public Transform m_castPoint;
    public float m_minDistance=0.1f;
    public float m_maxDistance=0.5f;
    public DistanceChangeEvent m_onDistanceChange;
    public DistanceChangeEvent m_onIntensityChange;
    [Header("Debug")]
    [SerializeField] float m_distanceFound;
    [SerializeField] float m_intensityFound;
    [SerializeField] bool m_collisionFound;
    [SerializeField] Vector3 m_collisionFoundPosition;
    void Update()
    {
        float nearestDistance = 0;
        CheckCollisionDistance(out m_collisionFound, out nearestDistance, out m_collisionFoundPosition);
        if (nearestDistance != m_distanceFound)
        {
            float intensity = 1f - Mathf.Clamp((nearestDistance - m_minDistance) / (m_maxDistance - m_minDistance), 0f, 1f);
            m_onIntensityChange.Invoke(intensity);
            m_onDistanceChange.Invoke(nearestDistance);
            m_intensityFound = intensity;
        }
        else if(!m_collisionFound){
            m_intensityFound = 0;
        }
        m_distanceFound = nearestDistance;
    }

    private void CheckCollisionDistance(out bool r_foundCollision,out float r_nearestDistance, out Vector3 r_nearestPoint)
    {
        r_nearestDistance = float.MaxValue;
        r_nearestPoint = m_castPoint.position;

        Collider [] colls=  Physics.OverlapSphere(m_castPoint.position, m_maxDistance, m_collisionChecker);
        r_foundCollision = colls.Length > 0;
        if (!r_foundCollision) 
            return;
        float distanceCollider;
        Vector3 closestCollider = new Vector3();
        for (int i = 0; i < colls.Length; i++)
        {
            closestCollider = colls[i].ClosestPoint(m_castPoint.position);
            distanceCollider = Vector3.Distance(closestCollider, m_castPoint.position);
            if (distanceCollider < r_nearestDistance)
            {
                r_nearestDistance = distanceCollider;
                r_nearestPoint = closestCollider;
            }

        }
    }

    [System.Serializable]
    public class DistanceChangeEvent : UnityEvent<float> { }
}
