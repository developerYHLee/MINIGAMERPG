using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    Transform player_transform;

    // Start is called before the first frame update
    void Start()
    {
        player_transform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player_transform.position.x, player_transform.position.y, transform.position.z);
    }
}
