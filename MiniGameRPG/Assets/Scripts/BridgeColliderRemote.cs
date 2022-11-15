using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeColliderRemote : MonoBehaviour
{
    public GameObject _bridgeTrigger1;

    //다리 안으로 들어갈 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<SpriteRenderer>().sortingLayerName = "Bridge";
        _bridgeTrigger1.SetActive(false);
    }

    //다리 밖으로 나갈 때
    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 2";
        _bridgeTrigger1.SetActive(true);
    }
}
