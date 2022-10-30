using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] _enemyPrefabs; // 0: µµÀû, 1: µµÀû¿Õ, 2: º´»ç1, 3: º´»ç2, 4: ¿Õ
    public EnemyData[] _enemyData;
    public GameObject _enemySpawner;
    DataController _dataController;

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

        if (_dataController.gameData._enemyIsSpawned)
        {
            for (int i = 0; i < _dataController._enemyDictionary.Count; i++)
            {
                LoadEnemy(i);
            }
        }

        else
        {
            _enemySpawner.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
