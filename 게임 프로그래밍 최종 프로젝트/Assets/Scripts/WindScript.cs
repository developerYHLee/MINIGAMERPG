using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public Material flag, ground;
    float radian = 0, power = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        radian = flag.GetFloat("_Degree") * Mathf.PI / 180;
        power = flag.GetFloat("_WindPower") / 20;
        transform.GetComponent<Renderer>().material = ground;
        transform.GetComponent<Renderer>().material.SetFloat("_Wind", radian);
        transform.GetComponent<Renderer>().material.SetFloat("_Rain", power);
    }
}
