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
        _explainText.text = "�÷��̾ �̵��ϴ� �����\n\nȭ���� ������ ���������� �̵��ϰ�,\n\nȭ���� ������ ���� ������ �������� �̵��Ѵ�.\n\nö�� ����, ������ ü�� ȸ���� ���δ�.";
        _startButton.SetActive(true);
        _nextTextButton.SetActive(false);
    }

    public void CloseResult()
    {
        _result.SetActive(false);

        //�÷��̾ ������ �� �ְ� ���ش�.
        _player.GetComponent<MoveCharacter>().enabled = true;

        //stage �� ����
        DataController.Instance.gameData._stage = _stage;

        //�� ����
        _enemySpawners[_stage].SetActive(true);
        
        //�ڽ� ��ü�� ������ 2, 3 spawner
        if (_enemySpawners[_stage].transform.childCount > 0)
        {
            for (int i = 0; i < _enemySpawners[_stage].transform.childCount; i++)
                _enemySpawners[_stage].transform.GetChild(i).GetComponent<EnemySpawner>().ReduceSetting(FallScoreScript.CountScore);
        }
        else _enemySpawners[_stage].GetComponent<EnemySpawner>().ReduceSetting(FallScoreScript.CountScore);

        //���� ����
        activeButton._countPotion += FallPotionScript.CountPotion;
        activeButton._countPotionText.text = "" + activeButton._countPotion;
        
        //���� ȸ���� 
        activeButton._healAmount += 20 * _stage;
        DataController.Instance.gameData._healAmount = activeButton._healAmount;
        activeButton._healAmountText.text = "Heal : " + activeButton._healAmount;

        //���� ��Ȳ ����
        FallScoreScript.CountScore = 0;
        FallPotionScript.CountPotion = 0;
    }
}
