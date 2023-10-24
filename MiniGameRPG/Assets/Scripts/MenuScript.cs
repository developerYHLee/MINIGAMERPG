using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject menu_background, resume_background, option_background, _manual, _fieldGate;
    public GameObject _nextStoryButton;
    public Text _hasToKill;
    Color _color;
    float _time;

    // Start is called before the first frame update
    void Start()
    {
        option_background.SetActive(false);

        _manual.SetActive(false);

        if (DataController.Instance.gameData._fieldGateOpen[0]) _fieldGate.SetActive(false);
        _color = _nextStoryButton.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //버튼 깜빡이기========
        if (_time < 0.5f)
        {
            _color.a = 1 - _time;
        }
        else
        {
            _color.a = _time;
            if (_time > 1f) _time = 0;
        }
        _nextStoryButton.GetComponent<Image>().color = _color;

        _time += Time.deltaTime;
        //====================

        _hasToKill.text = "남은 적 : " + GameObject.Find("GameManager").GetComponent<GameManager>()._hasToKill;
    }

    public void Toggle_Menu()
    {
        menu_background.SetActive(!menu_background.activeSelf);
        option_background.SetActive(false);
    }

    public void Toggle_Option()
    {
        option_background.SetActive(!option_background.activeSelf);
        menu_background.SetActive(!menu_background.activeSelf);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        resume_background.SetActive(false);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        resume_background.SetActive(true);
        menu_background.SetActive(false);
    }

    public void EraseData()
    {
        DataController.Instance.EraseGameData();

        SceneManager.LoadScene("StartScene");
    }

    public void CloseManual()
    {
        //설명창을 닫으면 시간이 흐른다.
        Time.timeScale = 1;
        
        _manual.SetActive(false);
        _fieldGate.SetActive(false);

        //문 열림
        DataController.Instance.gameData._fieldGateOpen[0] = true;
    }

    public void Quit()
    {
        Application.Quit();
        DataController.Instance.SaveGameData();
    }
}
