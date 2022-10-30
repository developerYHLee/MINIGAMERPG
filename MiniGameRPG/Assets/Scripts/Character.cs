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

    Vector2 pos, rot;
    public void TakeDamage(int damage)
    {
        HP -= damage;
        _hpSlider.value = HP;

        if(!IsDead()) _animator.SetTrigger("Hurt");
    }

    public bool IsDead()
    {
        if (HP <= 0)
        {
            _animator.SetBool("noBlood", true);
            if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("DeathNoBlood")) _animator.SetTrigger("Death");
            return true;
        }

        return false;
    }

    public void OnDead()
    {
        _animator.enabled = false;
        GetComponent<MoveCharacter>().enabled = false;
        gameObject.tag = "Untagged";

        //콜라이더 없애기
        BoxCollider2D[] boxCollider2D = GetComponents<BoxCollider2D>();
        for (int i = 0; i < 2; i++) boxCollider2D[i].enabled = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
