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

    //���� ����
    public Transform swordPos;
    public Vector2 boxSize;

    //�þ� ����
    public Transform _sightCircle;
    public float _sightRange;

    GameObject _player;
    float _attackTimer = 0f, _attackWaitingTime = 0.7f;

    //�ٶ󺸴� ����
    public bool _lookRight;

    Vector2 _movement;

    void Awake()
    {
        slider = HPBar.GetComponent<Slider>();
        animator = gameObject.GetComponent<Animator>();
        dataController = DataController.Instance;
    }

    private void Start()
    {
        //HPBar �ִ� ü�� ����
        slider.maxValue = _enemyData.Hp;
        slider.value = HP;

        _player = GameObject.Find("Player");
        _rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dis = Vector2.Distance(_sightCircle.position, _player.transform.position);

        //�÷��̾���� �Ÿ��� �þ� ������ ���Եǰ�, ���� �÷��̾ ��� ���� ���
        if (dis <= SightRange && HP > 0 && _player.GetComponent<Character>().HP > 0)
        {
            FaceTarget();

            if (dis >= 0.8f && !Attack()) MoveCharacter(_movement);
        }
        else
        {
            _rig2D.velocity = new Vector2(0, 0);
            animator.SetInteger("AniState", 0);
        }
    }

    private void Update()
    {
        Vector2 direction = _player.transform.position - _sightCircle.position;
        direction.Normalize();
        _movement = direction;
    }

    //����
    public bool Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(swordPos.position, boxSize, 0);

        //collider�� ������ �� collider ��
        foreach (Collider2D collider in collider2Ds)
        {
            //�±װ� Player�� Damage�� �ش�.
            if (collider.CompareTag("Player"))
            {
                //���� �����ϴ� �ִϸ��̼��� �������� �ƴ϶�� ���� �ִϸ��̼� ����
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    animator.SetTrigger("Attack");
                    animator.SetInteger("AniState", 2);
                }

                //������ ��Ÿ��
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= _attackWaitingTime)
                {
                    _attackTimer = 0f;
                    _player.GetComponent<Character>().TakeDamage(Damage);
                }

                return true;
            }
        }

        return false;
    }

    void MoveCharacter(Vector2 direction)
    {
        //�޸��� �ִϸ��̼�
        animator.SetInteger("AniState", 1);
        animator.SetTrigger("Run");

        _rig2D.MovePosition((Vector2)transform.position + (direction * MoveSpeed * Time.deltaTime));
    }

    //���� ü�� ��ȯ
    public void TakeDamage(int damage)
    {
        HP -= damage;
        slider.value = HP;

        isDead(false); //�������� �޴´ٴ� ���� ����ִٴ� ���� �ǹ��Ѵ�.
    }

    //�׾����� true, ������� false
    public bool isDead(bool isStartSpawned)
    {
        if (HP <= 0)
        {
            animator.SetBool("IsDead", true);
            
            HPBar.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            gameObject.tag = "Untagged";

            //���� �޴����� �Ѹ� �׾����� �˷��ش�. ��, ������ ������ �� �׾������� �������� �ʴ´�.
            if(!isStartSpawned) GameObject.Find("GameManager").GetComponent<GameManager>().DeadEnemy();

            //StartCoroutine(WaitForIt());
            return true;
        }

        //���� �ҷ����� �� ���� ����ִٸ� �׿��� �ϴ� �� ���� �÷��ش�.
        if (isStartSpawned) GameObject.Find("GameManager").GetComponent<GameManager>()._hasToKill++;

        return false;
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1f);
        //animator.enabled = false;
    }

    public void WatchEnemyInfo()
    {
        Debug.Log("�� �̸� : " + _enemyData.EnemyName);
        Debug.Log("�� ü�� : " + _enemyData.Hp);
        Debug.Log("�� ���ݷ� : " + _enemyData.Damage);
        Debug.Log("�� �þ� : " + _enemyData.SightRange);
        Debug.Log("�� �̵��ӵ� : " + _enemyData.MoveSpeed);
    }

    //�� ����, isSpawned�� true�� �̹� ������ ��ü;
    public void SetEnemy(EnemyGameData enemyGameData, bool isStartSpawned)
    {
        ID = enemyGameData._id;
        HP = enemyGameData._hp;
        Damage = enemyGameData._damage;
        Type = enemyGameData._type;
        SightRange = _enemyData.SightRange;
        MoveSpeed = _enemyData.MoveSpeed;

        isDead(isStartSpawned);
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

    //���ݹ����� ���̰� ��
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
