using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvoidPoopCanvasScript : MonoBehaviour
{
    public GameObject _background, _manual, _startButton, _nextTextButton, _result, _gameManager;
    public GameObject[] _enemySpawners;
    public Text _explainText, _resultScoreText, _resultPotionText, _resultDecreaseHpText, _resultDecreaseDamageText;
    public int _stage;

    GameObject _player;

    private void Start()
    {
        _background.SetActive(false);
        _manual.SetActive(false);
        _startButton.SetActive(false);
        _player = GameObject.Find("Player");
    }

    public void Toggle_GameStart()
    {
        _manual.SetActive(false);
        _background.SetActive(true);
    }

    public void NextText()
    {
        _explainText.text = "플레이어가 이동하는 방식은\n\n화면을 누르면 오른쪽으로 이동하고,\n\n화면을 누르고 있지 않으면 왼쪽으로 이동한다.\n\n철은 점수, 포션은 체력 회복에 쓰인다.";
        _startButton.SetActive(true);
        _nextTextButton.SetActive(false);
    }

    public void CloseResult()
    {
        _result.SetActive(false);

        //플레이어가 움직일 수 있게 해준다.
        _player.GetComponent<MoveCharacter>().enabled = true;

        //stage 값 저장
        DataController.Instance.gameData._stage = _stage;

        //적 생성
        _enemySpawners[_stage].SetActive(true);

        ActiveButton activeButton = GameObject.Find("JoyStickCanvas").GetComponent<ActiveButton>();

        //물약 개수
        activeButton._countPotion += FallPotionScript.CountPotion;
        activeButton._countPotionText.text = "" + activeButton._countPotion;
        
        //물약 회복량 
        activeButton._healAmount += 20 * _stage;
        activeButton._healAmountText.text = "Heal : " + activeButton._healAmount;
    }
}
