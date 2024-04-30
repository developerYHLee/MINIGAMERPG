using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSpotScript : MonoBehaviour
{
    public GameObject _avoidPoopManual, _avoidPoopCanvas, _gameManager;
    public int _stage, _hasToKillNum;
    GameObject _player;
    public bool _isLeft;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //똥피하기 메뉴얼
        _avoidPoopManual.SetActive(true);
        
        //플레이어 정지 및 이동불가
        _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        _player.GetComponent<MoveCharacter>().enabled = false;

        //점수 현황 지움
        FallScoreScript.CountScore = 0;
        FallPotionScript.CountPotion = 0;

        //스테이지 전달
        _avoidPoopCanvas.GetComponent<AvoidPoopCanvasScript>()._stage = _stage;
        _avoidPoopCanvas.GetComponent<AvoidPoopCanvasScript>()._isLeft = _isLeft;

        if (_stage == 1)
        {
            if (_isLeft) DataController.Instance.gameData._openLeftGate = true;
            else DataController.Instance.gameData._openRightGate = true;
        }

        gameObject.SetActive(false);
    }
}
