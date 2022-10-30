using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeColliderRemote : MonoBehaviour
{
    public GameObject _ColliderTop, _ColliderSideHide;
    private int _layer;
    private void OnTriggerExit2D(Collider2D other)
    {
        _layer = other.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        
        //�ٸ� ������ �� ��
        if(_layer == 0)
        {
            _ColliderTop.SetActive(false);
            _ColliderSideHide.SetActive(true);
        }

        //�ٸ� ������ ���� ��
        else if(_layer == 2)
        {
            _ColliderTop.SetActive(true);
            _ColliderSideHide.SetActive(false);
        }
    }
}
