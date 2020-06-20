using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitObjectInCameraView : MonoBehaviour
{
    public Camera m_cameraTargeted;
    public Transform m_targetToScale;
    public float m_multiplicator=1.1f;
    public float m_additionToNear = 0.001f;
    public float m_objectDepthSize = 0.1f;
    public bool m_affectTargetAtStart=true;
    public bool m_affectTargetAtUpdate;
    public void Start()
    {
        if (m_affectTargetAtStart)
            ScaleTheObjectToCameraView(m_targetToScale);

    }
    public void Update()
    {
        if (m_affectTargetAtStart)
            ScaleTheObjectToCameraView(m_targetToScale);

    }

    public  void ScaleTheObjectToCameraView(Transform objectToAffect)
    {
        float camNearDist = m_cameraTargeted.nearClipPlane + m_additionToNear;
        objectToAffect.rotation = m_cameraTargeted.transform.rotation;
        objectToAffect.position = m_cameraTargeted.transform.position + m_cameraTargeted.transform.forward *( camNearDist+ m_objectDepthSize/2f);
        Vector3 t, d, l, r;
        d = m_cameraTargeted.ViewportToWorldPoint(new Vector3(0.5f, 0f, camNearDist));
        t = m_cameraTargeted.ViewportToWorldPoint(new Vector3(0.5f, 1f, camNearDist));
        l = m_cameraTargeted.ViewportToWorldPoint(new Vector3(0, 0.5f, camNearDist));
        r = m_cameraTargeted.ViewportToWorldPoint(new Vector3(1, 0.5f, camNearDist));
        //Debug.DrawLine(d, t, Color.cyan, 20);
        //Debug.DrawLine(l, r, Color.blue, 20);
        objectToAffect.localScale =
            new Vector3(
                Vector3.Distance(l, r) * m_multiplicator,
            Vector3.Distance(t, d) * m_multiplicator,
            m_objectDepthSize);
    }
    private void OnValidate()
    {
        if (m_objectDepthSize == 0f)
            m_objectDepthSize = 0.00001f;
    }
}
