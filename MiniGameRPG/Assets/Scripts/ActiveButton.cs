using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ActiveButton : MonoBehaviour
{
    public GameObject _player;
    public Text _countPotionText, _healAmountText;
    Animator animator;
    bool isRoll, isStop, isAttack;
    float _rollTimer = 0.0f, _rollWaitingTime = 0.6f;

    //물약 개수
    public int _countPotion = 0, _healAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = _player.GetComponent<Animator>();
        isRoll = false; isStop = false;

        //포션 가져오기
        _countPotion = DataController.Instance.gameData._countPotion;
        _countPotionText.text = "" + _countPotion;
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
            _player.GetComponent<Character>().Attack();
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
            _player.GetComponent<Character>().SetAttackTimer();
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

    public void Heal()
    {
        if (_countPotion > 0)
        {
            _countPotionText.text = "" + --_countPotion;

            //데미지를 음수로 받으면 체력이 회복된다.
            _player.GetComponent<Character>().TakeDamage(-_healAmount);
        }
    }

    private void OnApplicationQuit()
    {
        DataController.Instance.gameData._countPotion = _countPotion;
    }
}