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
public class GateOn
{
    public bool _gameSpotOnLeft, _gameSpotOnRight, _gateOnLeft, _gateOnRight;

    public GateOn()
    {
        _gameSpotOnLeft = false;
        _gameSpotOnRight = false;
        _gateOnLeft = false;
        _gateOnRight = false;
    }

    public void set_gameSpotOnLeft() { _gameSpotOnLeft = true; }
    public void set_gameSpotOnRight() { _gameSpotOnRight = true; }
    public bool get_gameSpotOnLeft() { return _gameSpotOnLeft; }
    public bool get_gameSpotOnRight() { return _gameSpotOnRight; }
    public void set_gateOnLeft() { _gateOnLeft = true; }
    public void set_gateOnRight() { _gateOnRight = true; }
    public bool get_gateOnLeft() { return _gateOnLeft; }
    public bool get_gateOnRight() { return _gateOnRight; }
}

[Serializable]
public class GameData
{
    public int _player_hp, _player_mp, _player_damage, _player_level, _player_maxHp, _player_maxMp, _stage, _statUp, _countPotion, _healAmount;
    public List<float> _player_pos, _player_rot;
    public List<int> _enemy_id = new List<int>();
    public List<EnemyGameData> _enemy_enemyGameData = new List<EnemyGameData>();
    public bool _enemyIsSpawned = false;
    public List<bool> _fieldGateOpen;
    public List<bool> _miniGameIsCleared;
    public List<GateOn> _isLeft = new List<GateOn>();

    public GameData()
    {
        _player_maxHp = 100;
        _player_maxMp = 10;
        _player_hp = 100;
        _player_mp = 10;
        _player_damage = 20;
        _player_level = 1;

        _player_pos = new List<float>();
        _player_pos.Add(-5); _player_pos.Add(31.5f); //캐릭터 위치 x,y

        _player_rot = new List<float>();
        _player_rot.Add(0); _player_rot.Add(0); //캐릭터 회전 x,y

        _fieldGateOpen = new List<bool>();
        _miniGameIsCleared = new List<bool>();

        for (int i = 0; i < 7; i++)
        {
            _fieldGateOpen.Add(false);
            _miniGameIsCleared.Add(false);
        }

        _stage = 0;
        _statUp = 0;

        _countPotion = 0;
        _healAmount = 20;

        for (int i = 0; i < 2; i++) _isLeft.Add(new GateOn());
    }
}