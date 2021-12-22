using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour
{
    public Material flag;
    float degree = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        degree = flag.GetFloat("_Degree");
        transform.GetComponent<Renderer>().material.SetFloat("_Compass", degree);
    }
}
