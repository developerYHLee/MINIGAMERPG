using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCharacter : MonoBehaviour
{
    public static float speed = 2, max_speed = 3;
    private JoyStick joyStick;
    Rigidbody2D rig2D;
    Animator animator;
    float rot;

    void Awake()
    {
        joyStick = GameObject.FindObjectOfType<JoyStick>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float h = joyStick.Horizontal, v = joyStick.Vertical; //h : �¿�, v : ����
        float upSpeed = speed * Time.deltaTime * v, rightSpeed = speed * Time.deltaTime * h; //�̵� �ӵ�
        
        //�̵�
        rig2D.AddForce(new Vector2(rightSpeed, upSpeed), ForceMode2D.Impulse);

        //�ִ�ӵ�
        if (Mathf.Abs(rig2D.velocity.x) >= max_speed && Mathf.Abs(rig2D.velocity.y) >= max_speed) rig2D.velocity = new Vector2(max_speed * h, max_speed * v);
        //�¿� �ִ�ӵ�
        else if (Mathf.Abs(rig2D.velocity.x) >= max_speed) rig2D.velocity = new Vector2(max_speed * h, rig2D.velocity.y * v);
        //���� �ִ�ӵ�
        else if (Mathf.Abs(rig2D.velocity.y) >= max_speed) rig2D.velocity = new Vector2(rig2D.velocity.x * h, max_speed * v);

        //ĳ���� Rotation
        if (h > 0) rot = 0;
        else if (h < 0) rot = 180;
        transform.rotation = Quaternion.Euler(new Vector2(0, rot));

        //�ִϸ��̼�
        if (h == 0 && v == 0)
        {
            rig2D.velocity = new Vector2(0, 0);
            animator.SetInteger("AnimState", 0);
        }
        else animator.SetInteger("AnimState", 1);
    }
}
