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
        float h = joyStick.Horizontal, v = joyStick.Vertical; //h : 좌우, v : 상하
        float upSpeed = speed * Time.deltaTime * v, rightSpeed = speed * Time.deltaTime * h; //이동 속도
        
        //이동
        rig2D.AddForce(new Vector2(rightSpeed, upSpeed), ForceMode2D.Impulse);

        //최대속도
        if (Mathf.Abs(rig2D.velocity.x) >= max_speed && Mathf.Abs(rig2D.velocity.y) >= max_speed) rig2D.velocity = new Vector2(max_speed * h, max_speed * v);
        //좌우 최대속도
        else if (Mathf.Abs(rig2D.velocity.x) >= max_speed) rig2D.velocity = new Vector2(max_speed * h, rig2D.velocity.y * v);
        //상하 최대속도
        else if (Mathf.Abs(rig2D.velocity.y) >= max_speed) rig2D.velocity = new Vector2(rig2D.velocity.x * h, max_speed * v);

        //캐릭터 Rotation
        if (h > 0) rot = 0;
        else if (h < 0) rot = 180;
        transform.rotation = Quaternion.Euler(new Vector2(0, rot));

        //애니메이션
        if (h == 0 && v == 0)
        {
            rig2D.velocity = new Vector2(0, 0);
            animator.SetInteger("AnimState", 0);
        }
        else animator.SetInteger("AnimState", 1);
    }
}
