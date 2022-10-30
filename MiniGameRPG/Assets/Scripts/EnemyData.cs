using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    public Sprite enemyImage;
    
    [SerializeField]
    private string enemyName;
    public string EnemyName { get { return enemyName; } }

    public int EnemyType; //0: µµÀû, 1: µµÀû¿Õ, 2: º´»ç1, 3: º´»ç2, 4: ¿Õ
    
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }
    
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }
    
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    private float sightRange;
    public float SightRange { get { return sightRange; } }

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
}
