using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGateScript : MonoBehaviour
{
    public GameObject _manual;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //설명창이 열리면 시간이 멈춘다.
        Time.timeScale = 0;

        _manual.SetActive(true);
    }
}
