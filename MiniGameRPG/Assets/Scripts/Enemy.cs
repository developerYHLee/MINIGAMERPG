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
    float _attackTimer = 0f, _attackWaitingTime = 0.7f;

    //바라보는 방향
    public bool _lookRight;

    Vector2 _movement;

    void Awake()
    {
        Debug.Log(GetComponentsInChildren<BoxCollider2D>()[0].name);
        Physics2D.IgnoreCollision(GetComponentsInChildren<BoxCollider2D>()[0], GetComponentsInChildren<BoxCollider2D>()[1]);
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

        //플레이어와의 거리가 시야 범위에 포함되고, 적과 플레이어가 살아 있을 경우
        if (dis <= SightRange && HP > 0 && _player.GetComponent<Character>().HP > 0)
        {
            FaceTarget();
            Attack();
        }
        else
        {
            _attackTimer = 0f;
            _rig2D.velocity = new Vector2(0, 0);
            animator.SetInteger("AniState", 0);
        }
    }

    private void Update()
    {
        //플레이어 방향으로 단위 벡터
        Vector2 direction = _player.transform.position - _sightCircle.position;
        direction.Normalize();
        _movement = direction;
    }

    //공격
    public void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(swordPos.position, boxSize, 0);
        bool find_player = false;

        //collider에 오버랩 된 collider 중
        foreach (Collider2D collider in collider2Ds)
        {
            //태그가 Player면 Damage를 준다.
            if (collider.CompareTag("Player")) find_player = true;
        }

        if(find_player)
        {
            //현재 동작하는 애니매이션이 공격중이 아니라면 공격 애니매이션 시작
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                _attackTimer = 0f;
                animator.SetTrigger("Attack");
                animator.SetInteger("AniState", 2);
            }

            _attackTimer += Time.deltaTime;
            //데미지 쿨타임
            if (_attackTimer >= _attackWaitingTime)
            {
                _attackTimer = 0f;
                _player.GetComponent<Character>().TakeDamage(Damage);
            }
        }
        else MoveCharacter(_movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        //달리기 애니메이션
        animator.SetInteger("AniState", 1);
        animator.SetTrigger("Run");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;

        _rig2D.MovePosition((Vector2)transform.position + (direction * MoveSpeed * Time.deltaTime));
    }

    //남은 체력 반환
    public void TakeDamage(int damage)
    {
        HP -= damage;
        slider.value = HP;

        isDead(false); //데미지를 받는다는 것은 살아있다는 것을 의미한다.
    }

    //죽었으면 true, 살았으면 false
    public bool isDead(bool isLoadSpawned)
    {
        if (HP <= 0)
        {
            animator.SetBool("IsDead", true);
            
            HPBar.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            gameObject.transform.Find("KinematicBox").gameObject.SetActive(false);
            gameObject.transform.Find("EnemyHitBox").gameObject.SetActive(false);
            gameObject.tag = "Untagged";

            //게임 메니저로 한명 죽었음을 알려준다. 단, 게임이 시작할 때 죽어있으면 전달하지 않는다.
            if(!isLoadSpawned) GameObject.Find("GameManager").GetComponent<GameManager>().DeadEnemy();

            return true;
        }

        //적을 불러왔을 때 적이 살아있다면 죽여야 하는 적 수를 늘려준다.
        if (isLoadSpawned) GameObject.Find("GameManager").GetComponent<GameManager>()._hasToKill++;

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

    //적 설정, isSpawned가 true면 이미 생성된 객체;
    public void SetEnemy(EnemyGameData enemyGameData, bool isLoadSpawned)
    {
        ID = enemyGameData._id;
        HP = enemyGameData._hp;
        Damage = enemyGameData._damage;
        Type = enemyGameData._type;
        SightRange = _enemyData.SightRange;
        MoveSpeed = _enemyData.MoveSpeed;

        isDead(isLoadSpawned);
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
