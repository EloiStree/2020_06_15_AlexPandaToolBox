
using UnityEngine;

public class DarkView : MonoBehaviour
{

    [SerializeField] Material m_rendererToAffect;
    [SerializeField] Color choosedColor = Color.clear;

    public float m_lerpPower;
    [SerializeField] Color baseColor = Color.clear;
     Color wantedColor = Color.clear;
    public void Update()
    {
        if (baseColor != wantedColor) { 
            baseColor = Color.Lerp(baseColor, wantedColor, Time.deltaTime * m_lerpPower);
            m_rendererToAffect.color = baseColor;
        }
    }

    public void ChangeAlpha(float newAlphaInPourcent)
    {
        if (newAlphaInPourcent != wantedColor.a) {
            wantedColor = choosedColor;
            wantedColor.a = newAlphaInPourcent;

        }
    }
    
}
