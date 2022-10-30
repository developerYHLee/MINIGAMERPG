using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLayerScript : MonoBehaviour
{
    private int _layer;
    public GameObject _ColliderTop, _ColliderSideHide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _layer = other.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

        //다리 안으로 들어갈 때
        if (_layer == 2)
        {
            _ColliderTop.SetActive(false);
            _ColliderSideHide.SetActive(true);

            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        }

        //다리 밖으로 나올 때
        else if (_layer == 0)
        {
            _ColliderTop.SetActive(true);
            _ColliderSideHide.SetActive(false);

            other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 2";
        }
    }
}
