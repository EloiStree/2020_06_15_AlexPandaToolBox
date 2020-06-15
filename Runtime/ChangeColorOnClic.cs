using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnClic : MonoBehaviour
{
    public GameObject objectToChangeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            objectToChangeColor.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1);
        }
    }
}
