using UnityEngine;
using UnityEngine.Events;

public class RaycastEven : MonoBehaviour
{
    [Tooltip("Length max of the raycast")]
    public float detectionRange = 3;

    [Tooltip("The layer who trigger the event")]
    public LayerMask layer;

    public bool debugRation;

    public DistanceChangeEvent m_onDidistanceChange;

    private float m_previousDistance;
    public RaycastHit raycastHit;

    private float ratio;


    private void Update()
    {
        OnRaycasting();
    }

    private void OnRaycasting()     //if the raycast touches the appropriate layer, it launches the event
    {
        bool hasHit = Physics.Raycast(transform.position, transform.forward, out raycastHit, detectionRange, layer);
        if (hasHit && raycastHit.distance <= detectionRange && raycastHit.distance != m_previousDistance)
        {
            ratio = (1f - raycastHit.distance / detectionRange);
            m_onDidistanceChange.Invoke(ratio);
            m_previousDistance = raycastHit.distance;

            if(debugRation)
            Debug.LogWarning("Mon ratio est de  " + ratio);
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
