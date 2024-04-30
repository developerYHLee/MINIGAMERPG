using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAvoidPoopScript : MonoBehaviour
{
    //플레이어
    public GameObject _player;
    Rigidbody2D _rig2D;
    Animator _animator;

    //플레이어 이동속도
    float _moveSpeed = 0;

    //오브젝트
    private bool[] _check = { true, true, true }; //0 : Poop, 1 : Score, 2 : Potion
    public GameObject[] _objects; //0 : Poop, 1 : Score, 2 : Potion
    public GameObject _background, _objectsBox;
    RectTransform rT;

    //점수판
    public Text _scoreText, _potionText;
    public Coroutine[] _enumerator = new Coroutine[3];

    private void OnEnable()
    {
        for (int i = 0; i < _objectsBox.transform.childCount; i++) Destroy(_objectsBox.transform.GetChild(i).gameObject);
        for (int i = 0; i < _check.Length; i++) _check[i] = true;
        _moveSpeed = 0;
    }

    private void Start()
    {
        _rig2D = _player.GetComponent<Rigidbody2D>();
        _animator = _player.GetComponent<Animator>();
        rT = _background.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        _rig2D.AddForce(new Vector2(_moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        if (Math.Abs(_rig2D.velocity.x) >= Math.Abs(_moveSpeed)) _rig2D.velocity = new Vector2(_moveSpeed, 0);
    }

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_check[i]) _enumerator[i] = StartCoroutine(CreateObject(i));
        }

        _scoreText.text = ": " + FallScoreScript.CountScore;
        _potionText.text = ": " + FallPotionScript.CountPotion;
    }

    IEnumerator CreateObject(int num)
    {
        float ran = UnityEngine.Random.Range(-rT.rect.width / 2 + 75f, rT.rect.width / 2 - 75f); ;

        _check[num] = false;
        GameObject obj = Instantiate(_objects[num], new Vector2(0, 0), Quaternion.identity, _objectsBox.transform);
        //if (num == 0) obj.transform.Rotate(180, 0, 0);
        obj.GetComponent<RectTransform>().localPosition = new Vector2(ran, 500);
        yield return new WaitForSeconds(num * 2.3f + 0.2f);
        _check[num] = true;
    }

    //버튼이 안눌릴때
    public void MoveLeft()
    {
        _player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        _rig2D.velocity = new Vector2(0, 0);
        _moveSpeed = -5f;
    }

    //버튼이 눌릴 때
    public void MoveRight()
    {
        _animator.SetInteger("AnimState", 1);
        _player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        _rig2D.velocity = new Vector2(0, 0);
        _moveSpeed = 5f;
    }
}
