using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject function_background;
    public GameObject[] _uiList;
    public Text[] _amountTexts; //0: Hp, 1: Mp, 2: Damage, 3: StatUp
    public GameObject[] _statUpButton; //0: Hp, 1: Mp, 2: Damage
    public int _statUp;

    Character _character;

    // Start is called before the first frame update
    void Start()
    {
        _character = GameObject.Find("Player").GetComponent<Character>();

        _statUp = DataController.Instance.gameData._statUp;

        SetStats();

        //스텟 올리는 버튼을 활성화 할지 확인
        CheckStatUpButton();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Toggle_FunctionBackground()
    {
        function_background.SetActive(!function_background.activeSelf);
        for(int i = 0; i < _uiList.Length; i++) _uiList[i].SetActive(false);
    }

    //스탯창
    public void ToggleStatsList()
    {
        _uiList[0].SetActive(!_uiList[0].activeSelf);
    }

    //장비창
    public void ToggleEquipmentList()
    {
        _uiList[1].SetActive(!_uiList[1].activeSelf);
    }

    //인벤토리
    public void ToggleInventoryList()
    {
        _uiList[2].SetActive(!_uiList[2].activeSelf);
    }
    
    public void SetStats()
    {
        _amountTexts[0].text = "" + _character.MAXHP;
        _amountTexts[1].text = "" + _character.MAXMP;
        _amountTexts[2].text = "" + _character.Damage;
        _amountTexts[3].text = "" + _statUp;
    }

    //스탯 올리기
    public void ChangeStat(int num)
    {
        string text = "";

        if (num == 0)
        {
            _character.HP_StatUP();
            text += _character.MAXHP;
        }
        else if (num == 1)
        {
            _character.MP_StatUP();
            text += _character.MAXMP;
        }
        else if (num == 2)
        { 
            _character.Damage += 30;
            text += _character.Damage;
        }

        _amountTexts[num].text = text;

        _statUp--;
        DataController.Instance.gameData._statUp = _statUp;
        _amountTexts[3].text = "" + _statUp;

        CheckStatUpButton();
    }

    //스탯 포인트 증가
    public void PlusStatUp(int num)
    {
        //스텟 포인트를 올린 후 값 저장
        _statUp += num;
        DataController.Instance.gameData._statUp = _statUp;
        _amountTexts[3].text = "" + _statUp;

        CheckStatUpButton();
    }

    //스탯이 있다면 버튼 활성화
    public void CheckStatUpButton()
    {
        bool isEnable = false;
        if (_statUp > 0) isEnable = true;

        for (int i = 0; i < _statUpButton.Length; i++)
        {
            _statUpButton[i].GetComponent<Button>().enabled = isEnable;

            Color color = _statUpButton[i].GetComponent<Image>().color;

            //활성화 유무에 따른 알파값 변경
            if (isEnable)
            {
                color.a = 1f;
                _statUpButton[i].GetComponent<Image>().color = color;
            }
            else
            {
                color.a = 0.5f;
                _statUpButton[i].GetComponent<Image>().color = color;
            }
        }
    }
}