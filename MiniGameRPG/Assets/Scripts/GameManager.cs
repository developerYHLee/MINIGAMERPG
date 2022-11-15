using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyPrefabs; // 0: 도적, 1: 도적왕, 2: 병사1, 3: 병사2, 4: 왕
    public EnemyData[] _enemyData;
    DataController _dataController;

   public GameObject[] _miniGameSpot, _miniGameGate;

    //죽여야 하는 적의 수
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
            Debug.Log("적을 불러옴");

            for (int i = 0; i < _dataController._enemyDictionary.Count; i++)
            {
                LoadEnemy(i);
            }
        }

        //미니게임을 했으면, 해당 장소의 미니게임을 비활성화 시킨다.
        for (int i = 0; i < _dataController.gameData._miniGameIsCleared.Count; i++)
        {
            if (DataController.Instance.gameData._miniGameIsCleared[i]) _miniGameSpot[i].SetActive(false);
        }

        //문이 열렸으면 해당 문을 비활성화 시킨다.
        for (int i = 0; i < _dataController.gameData._fieldGateOpen.Count; i++)
        {
            if (DataController.Instance.gameData._fieldGateOpen[i]) _miniGameGate[i].SetActive(false);
        }
    }


    //적이 한명 죽을 때마다 실행하도록 한다.
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
