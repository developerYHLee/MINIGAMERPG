using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * h * 0.5f);
        transform.Translate(Vector3.forward * v * 0.5f);

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseX * 2);
        }
        if (Input.GetMouseButton(1))
        {
            float mouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.left * mouseY * 2);
        }
    }
}
