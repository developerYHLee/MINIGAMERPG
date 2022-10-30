using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData _enemyData;
    public EnemyData EnemyData { set { _enemyData = value; } }
    
    Slider slider;
    public GameObject HPBar;
    Rigidbody2D _rig2D;

    public int ID { set; get; }
    public int Type { set; get; }
    public int HP { set; get; }
    public int Damage { set; get; }
    public float SightRange { set; get; }
    public float MoveSpeed { set; get; }

    DataController dataController;

    Animator animator;

    //공격 범위
    public Transform swordPos;
    public Vector2 boxSize;

    //시야 범위
    public Transform _sightCircle;
    public float _sightRange;

    GameObject _player;
    float _attackTimer = 0f, _attackWaitingTime = 1f;

    //바라보는 방향
    public bool _lookRight;

    void Awake()
    {
        slider = HPBar.GetComponent<Slider>();
        animator = gameObject.GetComponent<Animator>();
        dataController = DataController.Instance;
    }

    private void Start()
    {
        //HPBar 최대 체력 설정
        slider.maxValue = _enemyData.Hp;
        slider.value = HP;

        _player = GameObject.Find("Player");
        _rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dis = Vector2.Distance(_sightCircle.position, _player.transform.position);

        if (dis <= SightRange && HP > 0)
        {
            FaceTarget();

            animator.SetInteger("AniState", 1);
            
            Attack();
            if (dis > Mathf.Abs(1.5f) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) MoveToTarget();
        }
        else
        {
            _rig2D.velocity = new Vector2(0, 0);
            animator.SetInteger("AniState", 0);
        }
    }

    //남은 체력 반환
    public void TakeDamage(int damage)
    {
        HP -= damage;
        slider.value = HP;

        isDead();
    }

    //죽었으면 true, 살았으면 false
    public bool isDead()
    {
        if (HP <= 0)
        {
            animator.SetBool("IsDead", true);
            HPBar.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            return true;
        }

        return false;
    }

    public void WatchEnemyInfo()
    {
        Debug.Log("적 이름 : " + _enemyData.EnemyName);
        Debug.Log("적 체력 : " + _enemyData.Hp);
        Debug.Log("적 공격력 : " + _enemyData.Damage);
        Debug.Log("적 시야 : " + _enemyData.SightRange);
        Debug.Log("적 이동속도 : " + _enemyData.MoveSpeed);
    }

    //적 설정
    public void SetEnemy(EnemyGameData enemyGameData)
    {
        ID = enemyGameData._id;
        HP = enemyGameData._hp;
        Damage = enemyGameData._damage;
        Type = enemyGameData._type;
        SightRange = _enemyData.SightRange;
        MoveSpeed = _enemyData.MoveSpeed;

        isDead();
    }

    //공격
    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(swordPos.position, boxSize, 0);

        //데미지 쿨타임
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackWaitingTime)
        {
            _attackTimer = 0f;

            //collider에 오버랩 된 collider 중 태그가 Player면 Damage를 준다.
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Player"))
                {
                    //현재 동작하는 애니매이션이 공격중이 아니라면 공격 애니매이션 시작
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) animator.SetInteger("AniState", 2);

                    _player.GetComponent<Character>().TakeDamage(Damage);
                }
            }
        }
    }

    void MoveToTarget()
    {
        float dir_x = _player.transform.position.x - _sightCircle.position.x;
        float dir_y = _player.transform.position.y - _sightCircle.position.y;

        //플레이어보다 왼쪽에 있으면 -1, 오른쪽에 있으면 1
        dir_x = dir_x < 0 ? -1 : 1;
        //플레이어보다 위에 있으면 -1, 아래에 있으면 1
        dir_y = dir_y < 0 ? -1 : 1;

        _rig2D.AddForce(new Vector2(dir_x, dir_y) * MoveSpeed * Time.deltaTime, ForceMode2D.Impulse);

        //최대속도
        if (Mathf.Abs(_rig2D.velocity.x) >= MoveSpeed && Mathf.Abs(_rig2D.velocity.y) >= MoveSpeed) _rig2D.velocity = new Vector2(MoveSpeed * dir_x, MoveSpeed * dir_y);
        //좌우 최대속도
        else if (Mathf.Abs(_rig2D.velocity.x) >= MoveSpeed) _rig2D.velocity = new Vector2(MoveSpeed * dir_x, _rig2D.velocity.y * dir_y);
        //상하 최대속도
        else if (Mathf.Abs(_rig2D.velocity.y) >= MoveSpeed) _rig2D.velocity = new Vector2(_rig2D.velocity.x * dir_x, MoveSpeed * dir_y);
    }

    void FaceTarget()
    {
        
        if(_player.transform.position.x - _sightCircle.position.x < 0)
        {
            Turn(180);
        }
        else
        {
            Turn(0);
        }
    }

    void Turn(int rot)
    {
        if (_lookRight) transform.rotation = Quaternion.Euler(new Vector3(0, rot));
        else transform.rotation = Quaternion.Euler(new Vector3(0, rot - 180));
    }

    //공격범위를 보이게 함
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(swordPos.position, boxSize);
        Gizmos.DrawWireSphere(_sightCircle.position, _sightRange);
    }

    private void OnApplicationQuit()
    {
        List<float> pos = new List<float>();
        pos.Add(transform.position.x); pos.Add(transform.position.y);

        dataController.SaveEnemy(ID, HP, Damage, pos, Type);
    }
}
