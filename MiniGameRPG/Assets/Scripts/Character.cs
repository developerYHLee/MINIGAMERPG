using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private int _MAXHP, _MAXMP;
    public int MAXHP { set; get; }
    public int MAXMP { set; get; }
    private int _HP, _MP;
    public int HP { set; get; }
    public int MP { set; get; }
    public int Damage { set; get; }
    public int Level { set; get; }

    //공격 범위
    public Transform swordPos;
    public Vector2 boxSize;

    Slider _hpSlider, _mpSlider;
    public GameObject _hpBar, _mpBar;

    //공격 시간 및 쿨타임
    float _attackTimer = 0.3f, _attackWaitingTime = 0.45f;

    Animator _animator;

    //캐릭터 위치, 회전
    Vector2 pos, rot;

    //캐릭터가 죽었을 때 나오는 UI
    public GameObject _onDeadButton;

    public bool _isUsingMP = false;
    public float _mpTimer = 1f;

    public void TakeDamage(int damage)
    {
        if (MAXHP <= HP - Damage)
        {
            Debug.Log("최대 체력을 초과할 수 없습니다! : " + MAXHP);
            HP = MAXHP;
            return;
        }

        if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Block")) HP -= damage;
        _hpSlider.value = HP;
        Debug.Log("공격 받음!");
        Debug.Log("현재 체력 : " + HP);

        IsDead();
    }

    public void UseMP(int amount)
    {
        MP -= amount;
        if (MP > MAXMP) MP = MAXMP;

        _mpSlider.value = MP;       
    }

    public bool IsDead()
    {
        if (HP <= 0)
        {
            _animator.SetBool("noBlood", true);
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("DeathNoBlood"))
            {
                _animator.SetTrigger("Death");
                OnDead();
            }
            return true;
        }

        return false;
    }

    public void OnDead()
    {
        GetComponent<MoveCharacter>().enabled = false;
        
        gameObject.tag = "Untagged";

        GetComponent<BoxCollider2D>().enabled = false;
        Damage = 0;

        _onDeadButton.SetActive(true);
    }

    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(swordPos.position, boxSize, 0);

        //데미지 쿨타임
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackWaitingTime)
        {
            _attackTimer = 0.0f;

            //collider에 오버랩 된 collider 중 태그가 Enemy면 Damage를 준다.
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.GetComponent<Enemy>().TakeDamage(Damage);
                    //_enemy = collider.GetComponent<Enemy>();
                }
            }
        }
    }

    public void SetAttackTimer()
    {
        _attackTimer = 0.3f;
    }

    void Awake()
    {
        _hpSlider = _hpBar.GetComponent<Slider>();
        _mpSlider = _mpBar.GetComponent<Slider>();
        
        Debug.Log("캐릭터 생성");
    }

    // Start is called before the first frame update
    void Start()
    {
        pos.x = DataController.Instance.gameData._player_pos[0];
        pos.y = DataController.Instance.gameData._player_pos[1];

        rot.x = DataController.Instance.gameData._player_rot[0];
        rot.y = DataController.Instance.gameData._player_rot[1];

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);

        MAXHP = DataController.Instance.gameData._player_maxHp;
        MAXMP = DataController.Instance.gameData._player_maxMp;
        HP = DataController.Instance.gameData._player_hp;
        MP = DataController.Instance.gameData._player_mp;
        Damage = DataController.Instance.gameData._player_damage;
        Level = DataController.Instance.gameData._player_level;

        //HPBar 최대 체력 설정
        _hpSlider.maxValue = DataController.Instance.gameData._player_maxHp;
        _hpSlider.value = HP;

        //MPBar 최대 체력 설정
        _mpSlider.maxValue = DataController.Instance.gameData._player_maxMp;
        _mpSlider.value = MP;

        _animator = GetComponent<Animator>();

        _onDeadButton.SetActive(false);

        IsDead();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isUsingMP)
        {
            _mpTimer -= Time.deltaTime;

            if (_mpTimer <= 0)
            {
                _mpTimer = 1f;

                UseMP(-1);
            }
        }
    }

    //공격범위를 보이게 함
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(swordPos.position, boxSize);
    }

    private void OnApplicationQuit()
    {
        List<float> pos = new List<float>();
        pos.Add(transform.position.x); pos.Add(transform.position.y);

        List<float> rot = new List<float>();
        rot.Add(transform.rotation.x); rot.Add(transform.rotation.y);

        DataController.Instance.SavePlayer(MAXHP, MAXMP, HP, MP, Damage, pos, rot, Level);
    }
}
