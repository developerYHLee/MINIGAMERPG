using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeColliderRemote : MonoBehaviour
{
    public GameObject _bridgeTrigger1;

    //�ٸ� ������ �� ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<SpriteRenderer>().sortingLayerName = "Bridge";
        _bridgeTrigger1.SetActive(false);
    }

    //�ٸ� ������ ���� ��
    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 2";
        _bridgeTrigger1.SetActive(true);
    }
}
