using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGateScript : MonoBehaviour
{
    public GameObject _manual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����â�� ������ �ð��� �����.
        Time.timeScale = 0;

        _manual.SetActive(true);
    }
}
