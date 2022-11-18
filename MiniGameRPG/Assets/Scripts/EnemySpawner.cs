using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Thief, ThiefBoss, Soldier, Boss
}

public class EnemySpawner : MonoBehaviour
{
    // _enemyPrefabs�� n��°�� _enemyPrefabsNumber�� n��°��ŭ ����
    [SerializeField]
    private List<EnemyData> _enemyDatas;
    [SerializeField]
    private List<GameObject> _enemyPrefabs;
    [SerializeField]
    private List<int> _enemyPrefabsNumber;

    // �������� ������ ��ġ�� ũ�⸦ ������ X
    [SerializeField]
    private float _randomSizeX;
    // �������� ������ ��ġ�� ũ�⸦ ������ Y
    [SerializeField]
    private float _randomSizeY;

    Transform _pos;
    float _randomX, _randomY;

    DataController dataController;

    public int _stage;

    private void Awake()
    {
        dataController = DataController.Instance;
    }

    void Start()
    {
        _pos = gameObject.transform;
        _stage = DataController.Instance.gameData._stage;

        for (int i = 0; i < _enemyPrefabs.Count; i++)
        {
            // _enemyPrefabsNumber�� i��°��ŭ ����
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
        var newEnemy = Instantiate(_enemyPrefabs[num], new Vector3(_randomX, _randomY, 0), Quaternion.identity, gameObject.transform).GetComponent<Enemy>();
        newEnemy.EnemyData = _enemyDatas[num];

        // �� ������ �� Dictionary�� ����.
        EnemyGameData enemyGameData = dataController.Write_EnemyData(_enemyDatas[num].Hp - FallScoreScript.CountScore * 10 * (_stage + 1), _enemyDatas[num].Damage - FallScoreScript.CountScore * 3 * (_stage + 1), _randomX, _randomY, _enemyDatas[num].EnemyType); // 0: ����, 1: ������, 2: ����1, 3: ����2, 4: ��
        newEnemy.SetEnemy(enemyGameData, false); //ó�� ���� �� ���

        return newEnemy;
    }
}