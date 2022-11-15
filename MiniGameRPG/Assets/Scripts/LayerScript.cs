using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerScript : MonoBehaviour
{
    private float _x_pos, _y_pos;

    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();    
    }

    // Update is called once per frame
    void Update()
    {
        _x_pos = tf.position.x;
        _y_pos = tf.position.y;
        tf.position = new Vector3(_x_pos, _y_pos, _y_pos + 100);
    }
}
