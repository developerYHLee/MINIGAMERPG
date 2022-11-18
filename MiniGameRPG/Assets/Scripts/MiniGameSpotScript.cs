using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSpotScript : MonoBehaviour
{
    public GameObject _avoidPoopManual, _avoidPoopCanvas, _gameManager;
    public int _stage, _hasToKillNum;
    GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����ϱ� �޴���
        _avoidPoopManual.SetActive(true);
        
        //�÷��̾� ���� �� �̵��Ұ�
        _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        _player.GetComponent<MoveCharacter>().enabled = false;

        //���� ��Ȳ ����
        FallScoreScript.CountScore = 0;
        FallPotionScript.CountPotion = 0;

        //���� �޴����� �׿��� �ϴ� �� �� ����
        _gameManager.GetComponent<GameManager>()._hasToKill += _hasToKillNum;

        //�������� ����
        _avoidPoopCanvas.GetComponent<AvoidPoopCanvasScript>()._stage = _stage;
    }
}
