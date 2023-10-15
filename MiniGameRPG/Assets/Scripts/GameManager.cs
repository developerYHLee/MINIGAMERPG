using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyPrefabs; // 0: ����, 1: ������, 2: ����1, 3: ����2, 4: ��
    public EnemyData[] _enemyData;
    DataController _dataController;
    public GameObject[] _miniGameSpot, _miniGameGate, _enemySpawners;

    //�׿��� �ϴ� ���� ��
    public int _hasToKill = 0;

    public int _stage;

    GameObject _buttonCanvas;

    public List<GateOn> _isLeft;
    public bool _openLeftGate, _openRightGate;

    public void LoadEnemy(int num)
    {

        EnemyGameData enemyGameData = _dataController._enemyDictionary[num];
        Vector3 pos = new Vector3(enemyGameData._pos_x, enemyGameData._pos_y, 0);

        Enemy enemy = Instantiate(_enemyPrefabs[enemyGameData._type], pos, Quaternion.identity).GetComponent<Enemy>();
        enemy.EnemyData = _enemyData[enemyGameData._type];
        enemy.SetEnemy(enemyGameData, true); //�ѹ� ������ ��
    }

    private void OnEnable()
    {
        _hasToKill = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _dataController = DataController.Instance;
        _buttonCanvas = GameObject.Find("ButtonCanvas");
        _stage = DataController.Instance.gameData._stage;
        _isLeft = DataController.Instance.gameData._isLeft;

        if (_dataController.gameData._enemyIsSpawned)
        {
            Debug.Log("���� �ҷ���");

            for (int i = 0; i < _dataController._enemyDictionary.Count; i++)
            {
                LoadEnemy(i);
            }
        }

        //�̴ϰ����� ������, �ش� ����� �̴ϰ����� ��Ȱ��ȭ ��Ų��.
        for (int i = 0; i < _dataController.gameData._miniGameIsCleared.Count; i++)
        {
            if (DataController.Instance.gameData._miniGameIsCleared[i])
            {
                if (_miniGameSpot[i].transform.childCount > 0)
                {
                    if (_isLeft[i - 1].get_gameSpotOnLeft()) _miniGameSpot[i].transform.GetChild(0).gameObject.SetActive(false);
                    if (_isLeft[i - 1].get_gameSpotOnRight()) _miniGameSpot[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else _miniGameSpot[i].SetActive(false);
            }
        }

        //���� �������� �ش� ���� ��Ȱ��ȭ ��Ų��.
        for (int i = 0; i < _dataController.gameData._fieldGateOpen.Count; i++)
        {
            if (DataController.Instance.gameData._fieldGateOpen[i])
            {
                if (i != 0 && _miniGameGate[i].transform.childCount > 0)
                {
                    if (_isLeft[i - 1].get_gateOnLeft()) _miniGameGate[i].transform.GetChild(0).gameObject.SetActive(false);
                    if (_isLeft[i - 1].get_gateOnRight()) _miniGameGate[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else _miniGameGate[i].SetActive(false);
            }
        }
    }

    //���� �Ѹ� ���� ������ �����ϵ��� �Ѵ�.
    public void DeadEnemy()
    {
        if (--_hasToKill <= 0)
        {
            _stage = DataController.Instance.gameData._stage;
 
            if(_stage == 1)
            {
                if (_openLeftGate)
                {
                    _miniGameGate[_stage + 1].transform.GetChild(0).gameObject.SetActive(false);
                    DataController.Instance.gameData._isLeft[1].set_gateOnLeft();
                }
                else if (_openRightGate)
                {
                    _miniGameGate[_stage + 1].transform.GetChild(1).gameObject.SetActive(false);
                    DataController.Instance.gameData._isLeft[1].set_gateOnRight();
                }
            }
            else
            {
                if(_stage == 0)
                {
                    DataController.Instance.gameData._isLeft[0].set_gateOnLeft();
                    DataController.Instance.gameData._isLeft[0].set_gateOnRight();
                }

                _miniGameGate[_stage + 1].SetActive(false);
            }

            _dataController.gameData._fieldGateOpen[_stage + 1] = true;
            _buttonCanvas.GetComponent<ButtonScript>().PlusStatUp(_stage + 1);

            Debug.Log("HasToKill : " + _hasToKill + " Stage : " + (_stage + 1));
        }
    }
}
