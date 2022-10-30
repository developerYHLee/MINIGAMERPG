using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Thief, ThiefBoss, Soldier, Boss
}

public class EnemySpawner : MonoBehaviour
{
    // _enemyPrefabs의 n번째는 _enemyPrefabsNumber의 n번째만큼 생성
    [SerializeField]
    private List<EnemyData> _enemyDatas;
    [SerializeField]
    private List<GameObject> _enemyPrefabs;
    [SerializeField]
    private List<int> _enemyPrefabsNumber;

    // 랜덤으로 생성될 위치의 크기를 정해줄 X
    [SerializeField]
    private float _randomSizeX;
    // 랜덤으로 생성될 위치의 크기를 정해줄 Y
    [SerializeField]
    private float _randomSizeY;

    Transform _pos;
    float _randomX, _randomY;

    DataController dataController;

    private void Awake()
    {
        dataController = DataController.Instance;
    }

    void Start()
    {
        _pos = gameObject.transform;

        for (int i = 0; i < _enemyPrefabs.Count; i++)
        {
            // _enemyPrefabsNumber의 i번째만큼 생성
            for (int j = 0; j < _enemyPrefabsNumber[i]; j++)
            {
                _randomX = Random.Range(_pos.position.x - _randomSizeX, _pos.position.x + _randomSizeX);
                _randomY = Random.Range(_pos.position.y - _randomSizeY, _pos.position.y + _randomSizeY);

                SpawnEnemy(i);
            }
        }  
    }

    public Enemy SpawnEnemy(int num)
    {
        var newEnemy = Instantiate(_enemyPrefabs[num], new Vector3(_randomX, _randomY, 0), Quaternion.identity).GetComponent<Enemy>();
        newEnemy.EnemyData = _enemyDatas[num];

        // 적 정보를 적 Dictionary에 쓴다.
        EnemyGameData enemyGameData = dataController.Write_EnemyData(_enemyDatas[num].Hp, _enemyDatas[num].Damage, _randomX, _randomY, _enemyDatas[num].EnemyType); // 0: 도적, 1: 도적왕, 2: 병사1, 3: 병사2, 4: 왕
        newEnemy.SetEnemy(enemyGameData);

        return newEnemy;
    }
}