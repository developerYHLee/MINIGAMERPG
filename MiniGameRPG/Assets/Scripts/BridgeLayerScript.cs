using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLayerScript : MonoBehaviour
{
    public GameObject _ColliderTop, _ColliderSideHide, _bridgeTrigger2;

    //다리 안으로 들어갈 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        _ColliderTop.SetActive(false);
        _ColliderSideHide.SetActive(true);
        _bridgeTrigger2.SetActive(false);

        other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
    }

    //다리 밖으로 나갈 때
    private void OnTriggerExit2D(Collider2D other)
    {
        _ColliderTop.SetActive(true);
        _ColliderSideHide.SetActive(false);
        _bridgeTrigger2.SetActive(true);

        other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 2";
    }
}
