using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyPrefabs; // 0: ����, 1: ������, 2: ����1, 3: ����2, 4: ��
    public EnemyData[] _enemyData;
    DataController _dataController;

   public GameObject[] _miniGameSpot, _miniGameGate;

    //�׿��� �ϴ� ���� ��
    public int _HasToKill = 0;

    public int _stage;

    GameObject _buttonCanvas;

    public void LoadEnemy(int num)
    {

        EnemyGameData enemyGameData = _dataController._enemyDictionary[num];
        Vector3 pos = new Vector3(enemyGameData._pos_x, enemyGameData._pos_y, 0);

        Enemy enemy = Instantiate(_enemyPrefabs[enemyGameData._type], pos, Quaternion.identity).GetComponent<Enemy>();
        enemy.EnemyData = _enemyData[enemyGameData._type];
        enemy.SetEnemy(enemyGameData);
    }

    // Start is called before the first frame update
    void Start()
    {
        _dataController = DataController.Instance;
        _buttonCanvas = GameObject.Find("ButtonCanvas");
        _stage = DataController.Instance.gameData._stage;

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
            if (DataController.Instance.gameData._miniGameIsCleared[i]) _miniGameSpot[i].SetActive(false);
        }

        //���� �������� �ش� ���� ��Ȱ��ȭ ��Ų��.
        for (int i = 0; i < _dataController.gameData._fieldGateOpen.Count; i++)
        {
            if (DataController.Instance.gameData._fieldGateOpen[i]) _miniGameGate[i].SetActive(false);
        }
    }


    //���� �Ѹ� ���� ������ �����ϵ��� �Ѵ�.
    public void DeadEnemy()
    {
        if (--_HasToKill <= 0)
        {
            _miniGameGate[_stage + 1].SetActive(false);
            _dataController.gameData._fieldGateOpen[_stage + 1] = true;
            _buttonCanvas.GetComponent<ButtonScript>().PlusStatUp(_stage + 1);
        }
    }
}
