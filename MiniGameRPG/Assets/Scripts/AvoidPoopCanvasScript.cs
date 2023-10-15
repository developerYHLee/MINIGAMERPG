using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvoidPoopCanvasScript : MonoBehaviour
{
    public GameObject _background, _manual, _startButton, _nextTextButton, _result, _gameManager;
    public GameObject[] _enemySpawners, _miniGameSpot, _miniGameGate;
    public Text _explainText, _resultScoreText, _resultPotionText, _resultDecreaseHpText, _resultDecreaseDamageText;
    public int _stage;
    public bool _isLeft;

    GameObject _player;

    ActiveButton activeButton;

    private void Start()
    {
        _background.SetActive(false);
        _manual.SetActive(false);
        _startButton.SetActive(false);
        _player = GameObject.Find("Player");

        activeButton = GameObject.Find("JoyStickCanvas").GetComponent<ActiveButton>();

        activeButton._healAmount = DataController.Instance.gameData._healAmount;
        activeButton._healAmountText.text = "Heal : " + activeButton._healAmount;
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

        //자식 객체가 있으면 2, 3 spawner
        if (_enemySpawners[_stage].transform.childCount > 0)
        {
            if (_isLeft)
            {
                Debug.Log(_enemySpawners[_stage].transform.GetChild(0).gameObject.name);
                _enemySpawners[_stage].transform.GetChild(0).gameObject.SetActive(true);
                _enemySpawners[_stage].transform.GetChild(0).GetComponent<EnemySpawner>().ReduceSetting(FallScoreScript.CountScore);
                
                DataController.Instance.gameData._isLeft[_stage - 1].set_gameSpotOnLeft();
                GameObject.Find("GameManager").GetComponent<GameManager>()._openLeftGate = true;

                _miniGameSpot[_stage].transform.GetChild(0).gameObject.SetActive(false);
                _miniGameGate[_stage].transform.GetChild(0).gameObject.SetActive(false);

            }
            else
            {
                Debug.Log(_enemySpawners[_stage].transform.GetChild(1).gameObject.name);
                _enemySpawners[_stage].transform.GetChild(1).gameObject.SetActive(true);
                _enemySpawners[_stage].transform.GetChild(1).GetComponent<EnemySpawner>().ReduceSetting(FallScoreScript.CountScore);
                
                DataController.Instance.gameData._isLeft[_stage - 1].set_gameSpotOnRight();
                GameObject.Find("GameManager").GetComponent<GameManager>()._openRightGate = true;

                _miniGameSpot[_stage].transform.GetChild(1).gameObject.SetActive(false);
                _miniGameGate[_stage].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else
        {
            //적 생성
            _enemySpawners[_stage].SetActive(true);
            _enemySpawners[_stage].GetComponent<EnemySpawner>().ReduceSetting(FallScoreScript.CountScore);

            _miniGameSpot[_stage].SetActive(false);
            _miniGameGate[_stage].SetActive(false);
        }

        //물약 개수
        activeButton._countPotion += FallPotionScript.CountPotion;
        activeButton._countPotionText.text = "" + activeButton._countPotion;
        
        //물약 회복량 
        activeButton._healAmount += 20 * _stage;
        DataController.Instance.gameData._healAmount = activeButton._healAmount;
        activeButton._healAmountText.text = "Heal : " + activeButton._healAmount;

        //점수 현황 지움
        FallScoreScript.CountScore = 0;
        FallPotionScript.CountPotion = 0;
    }
}
