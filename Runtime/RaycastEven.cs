using UnityEngine;
using UnityEngine.Events;

public class RaycastEven : MonoBehaviour
{
    public float detectionRange = 3;


    public LayerMask layer;

    public DistanceChangeEvent m_onDidistanceChange;

    private float m_previousDistance;
    public RaycastHit raycastHit;

    private float ratio;


    private void Update()
    {
        OnRaycasting();
    }

    private void OnRaycasting()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out raycastHit, detectionRange, layer);
        if (hasHit && raycastHit.distance <= detectionRange && raycastHit.distance != m_previousDistance)
        {
            ratio = (1f - raycastHit.distance / detectionRange);
            m_onDidistanceChange.Invoke(ratio);
            m_previousDistance = raycastHit.distance;

            Debug.LogWarning("Mon ratio est de  " + ratio);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + raycastHit.distance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);

    }
}
[System.Serializable]
public class DistanceChangeEvent : UnityEvent<float> { }
