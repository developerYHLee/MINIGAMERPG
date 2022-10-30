using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class EnemyGameData
{
    public int _id;
    public int _hp, _damage;
    public float _pos_x, _pos_y;
    public static int Count = 0;
    public int _type;
    public bool _isSpawned = false;
    
    public EnemyGameData() { }

    public EnemyGameData(int hp, int damage, float pos_x, float pos_y, int type)
    {
        _hp = hp;
        _damage = damage;
        _id = Count++;
        _pos_x = pos_x;
        _pos_y = pos_y;
        _type = type;
    }
}

[Serializable]
public class GameData
{
    public int _player_hp, _player_mp, _player_damage, _player_level, _player_maxHp, _player_maxMp;
    public List<float> _player_pos, _player_rot;
    public List<int> _enemy_id = new List<int>();
    public List<EnemyGameData> _enemy_enemyGameData = new List<EnemyGameData>();
    public bool _enemyIsSpawned = false;

    public GameData()
    {
        _player_maxHp = 100;
        _player_maxMp = 50;
        _player_hp = 100;
        _player_mp = 50;
        _player_damage = 20;
        _player_level = 1;

        _player_pos = new List<float>();
        _player_pos.Add(-5); _player_pos.Add(31.5f); //캐릭터 위치 x,y

        _player_rot = new List<float>();
        _player_rot.Add(0); _player_rot.Add(0); //캐릭터 회전 x,y
    }
}
