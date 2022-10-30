using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveButton : MonoBehaviour
{
    Animator animator;
    bool isRoll, isStop, isAttack;
    float _rollTimer = 0.0f, _rollWaitingTime = 0.6f;

    Character player;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<Character>();
        isRoll = false; isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStop)
        {
            isStop = false;
            animator.SetBool("Stop", true);
            animator.SetBool("IdleBlock", false);

            MoveCharacter.speed = 2;
            MoveCharacter.max_speed = 3;
        }

        //구르기 쿨타임
        if (isRoll)
        {
            _rollTimer += Time.deltaTime;
            if (_rollTimer >= _rollWaitingTime)
            {
                _rollTimer = 0.0f;
                isRoll = false;
                MoveCharacter.speed = 1;
                MoveCharacter.max_speed = 3;
            }
        }

        else if(isAttack)
        {
            player.Attack();
        }
    }

    public void StartAttackAnimation()
    {
        if (!isRoll)
        {
            animator.SetBool("Stop", false);
            animator.SetTrigger("Attack");
            isAttack = true;

            MoveCharacter.speed = 1;
            MoveCharacter.max_speed = 2;
        }
    }

    public void StartBlockAnimation()
    {
        if (!isRoll)
        {
            animator.SetBool("IdleBlock", true);
            animator.SetTrigger("Block");

            MoveCharacter.speed = 1;
            MoveCharacter.max_speed = 2;
        }
    }

    public void StopAnimation()
    {
        if (!isRoll)
        {
            isStop = true;

            //Attack 초기화
            isAttack = false;
            player.SetAttackTimer();
        }
    }


    public void RollAnimation()
    {
        if (!isRoll)
        {
            isRoll = true;
            animator.SetTrigger("Roll");
            MoveCharacter.speed *= 1.5f;
            MoveCharacter.max_speed *= 1.5f;
        }
    }

    public void StartDash()
    {
        if (!isRoll)
        {
            MoveCharacter.speed = 4;
            MoveCharacter.max_speed = 5;
        }
    }

    public void StopDash()
    {
        if (!isRoll)
        {
            MoveCharacter.speed = 1;
            MoveCharacter.max_speed = 3;
        }
    }
}