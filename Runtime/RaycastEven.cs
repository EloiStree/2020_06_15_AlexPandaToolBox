using UnityEngine;
using UnityEngine.Events;

public class RaycastEven : MonoBehaviour
{
    public float detectionRange = 3;

    private float m_PreviousDistance;

    public LayerMask layer;

    public RaycastHit raycastHit;

    public DistanceChangeEvent m_onDidistanceChange;

    public float ratioTest;


    private void Update()
    {
        OnRaycasting();
    }

    private void OnRaycasting()
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out raycastHit, detectionRange, layer);
        if (hasHit && raycastHit.distance <= detectionRange && raycastHit.distance != m_PreviousDistance)
        {
            ratioTest = (1f - raycastHit.distance / detectionRange);
            m_onDidistanceChange.Invoke(ratioTest);
            m_PreviousDistance = raycastHit.distance;

            Debug.LogWarning("Mon ratio est de  " + ratioTest);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);

    }
}
[System.Serializable]
public class DistanceChangeEvent : UnityEvent<float> { }
