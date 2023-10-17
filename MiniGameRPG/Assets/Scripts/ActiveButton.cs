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
    bool isRoll, isStop, isAttack, _mpUse;
    float _rollTimer = 0.0f, _rollWaitingTime = 0.6f, _mpUseTimer = 0f;

    //���� ����
    public int _countPotion = 0, _healAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = _player.GetComponent<Animator>();
        isRoll = false; isStop = false; _mpUse = false;

        //���� ��������
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

        //������ ��Ÿ��
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

        if (_mpUse)
        {
            _mpUseTimer += Time.deltaTime;

            if (_mpUseTimer >= 1f)
            {
                _mpUseTimer = 0f;
                _player.GetComponent<Character>().UseMP(1);
            }
        }
    }

    public void StartAttackAnimation()
    {
        if (!isRoll && _player.tag != "Untagged")
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
       UseMP();
       if (!isRoll && _player.tag != "Untagged" && _player.GetComponent<Character>().MP > 0)
        {
            animator.SetBool("IdleBlock", true);
            animator.SetTrigger("Block");

            MoveCharacter.speed = 1;
            MoveCharacter.max_speed = 2;
        }
    }

    public void StopAnimation()
    {
        _mpUse = false;
        _mpUseTimer = 0f;

        if (!isRoll)
        {
            isStop = true;

            //Attack �ʱ�ȭ
            isAttack = false;
            _player.GetComponent<Character>().SetAttackTimer();
        }
    }


    public void RollAnimation()
    {
        if (!isRoll && _player.tag != "Untagged")
        {
            isRoll = true;
            animator.SetTrigger("Roll");
            MoveCharacter.speed *= 1.5f;
            MoveCharacter.max_speed *= 1.5f;
        }
    }

    public void StartDash()
    {
        UseMP();
        if (!isRoll && _player.GetComponent<Character>().MP > 0)
        {
            MoveCharacter.speed = 4;
            MoveCharacter.max_speed = 5;
        }
    }

    public void StopDash()
    {
        _mpUse = false;
        _mpUseTimer = 0f;

        if (!isRoll)
        {
            MoveCharacter.speed = 1;
            MoveCharacter.max_speed = 3;
        }
    }

    public void Heal()
    {
        if (_countPotion > 0 && _player.tag != "Untagged")
        {
            _countPotionText.text = "" + --_countPotion;

            //�������� ������ ������ ü���� ȸ���ȴ�.
            _player.GetComponent<Character>().TakeDamage(-_healAmount);
        }
    }

    void UseMP()
    {
        _mpUse = true;
        _player.GetComponent<Character>().UseMP(1);
    }

    private void OnApplicationQuit()
    {
        DataController.Instance.gameData._countPotion = _countPotion;
    }
}