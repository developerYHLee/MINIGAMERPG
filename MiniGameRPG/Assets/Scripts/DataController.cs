using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataController : MonoBehaviour
{
    //싱글톤=============
    static GameObject _container;
    static GameObject Container { get { return _container; } }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent<DataController>();
                //_instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }

            return _instance;
        }
    }
    //==================

    public string GameDataFileName = "Savefile.json"; //이름, 절대 변경 X

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }

            return _gameData;
        }
    }

    public Dictionary<int, EnemyGameData> _enemyDictionary = new Dictionary<int, EnemyGameData>();
    public List<int> _spareEnemyId = new List<int>();
    public List<EnemyGameData> _spareEnemyGameData = new List<EnemyGameData>();

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공 : " + Application.persistentDataPath + GameDataFileName);

            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);

            foreach (int id in gameData._enemy_id)
            {
                if (!_enemyDictionary.ContainsKey(id))
                {
                    _enemyDictionary.Add(id, gameData._enemy_enemyGameData[id]);
                }
            }
        }

        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData();
        }   
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
    }

    public void EraseGameData()
    {
        //적 데이터 지움
        EnemyGameData.Count = 0;
        _enemyDictionary.Clear();
        _spareEnemyId.Clear();
        _spareEnemyGameData.Clear();

        //점수 현황 지움
        FallScoreScript.CountScore = 0;
        FallPotionScript.CountPotion = 0;

        //스테이지 초기화
        gameData._stage = 0;

        string filePath = Application.persistentDataPath + GameDataFileName;
        File.Delete(filePath);
        _gameData = null;

        Debug.Log("삭제 완료");
    }

    public void SavePlayer(int maxHp, int maxMp, int hp, int mp, int damage, List<float> pos, List<float> rot, int level)
    {
        gameData._player_maxHp = maxHp;
        gameData._player_maxMp = maxMp;
        gameData._player_hp = hp;
        gameData._player_mp = mp;
        gameData._player_damage = damage;
        gameData._player_level = level;
    
        for(int i = 0; i < 2; i++)
        {
            gameData._player_pos[i] = pos[i];
            gameData._player_rot[i] = rot[i];
        }

        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        
        Debug.Log("캐릭터 저장 완료");
    }

    public void SaveEnemy(int id, int hp, int damage, List<float> pos, int type)
    {
        Debug.Log("Enemy id : " + id);
        EnemyGameData enemyGameData = _enemyDictionary[id];

        enemyGameData._hp = hp;
        enemyGameData._damage = damage;

        enemyGameData._pos_x = pos[0];
        enemyGameData._pos_y = pos[1];

        enemyGameData._type = type;

        //적 데이터 저장
        if (!_enemyDictionary[id]._isSpawned)
        {
            gameData._enemy_id.Add(_spareEnemyId[id]);
            gameData._enemy_enemyGameData.Add(_spareEnemyGameData[id]);
        }
        _enemyDictionary[id]._isSpawned = true;
        gameData._enemyIsSpawned = true;

        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        
        Debug.Log("적 저장 완료");
    }

    public EnemyGameData Write_EnemyData(int hp, int damage, float pos_x, float pos_y, int type) //0: 도적, 1: 도적왕, 2: 병사1, 3: 병사2, 4: 왕
    {
        //공격력이 1보다 작으면 1로 설정
        damage = damage < 1 ? 1 : damage;
        
        EnemyGameData enemyGameData = new EnemyGameData(hp, damage, pos_x, pos_y, type);

        //적 Dictionary에 값 저장
        if (!_enemyDictionary.ContainsKey(enemyGameData._id))
        {
            _spareEnemyId.Add(enemyGameData._id);
            _spareEnemyGameData.Add(enemyGameData);
            _enemyDictionary.Add(enemyGameData._id, enemyGameData);
        }

        return enemyGameData;
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}