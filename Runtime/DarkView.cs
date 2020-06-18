
using System.Runtime.CompilerServices;
using UnityEngine;

public class DarkView : MonoBehaviour
{

    Color baseColor = Color.clear;

    public void ChangeAlpha(float ratio)
    {
        gameObject.GetComponent<Renderer>().material.color = baseColor;
        baseColor.a = ratio;

        Debug.Log("My alpha = " + ratio);

    }
}
