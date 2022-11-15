using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu_background, resume_background, option_background, _manual, _fieldGate;

    // Start is called before the first frame update
    void Start()
    {
        option_background.SetActive(false);

        _manual.SetActive(false);

        if (DataController.Instance.gameData._fieldGateOpen[0]) _fieldGate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle_Menu()
    {
        menu_background.SetActive(!menu_background.activeSelf);
    }

    public void Toggle_Option()
    {
        option_background.SetActive(!option_background.activeSelf);
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
        //����â�� ������ �ð��� �帥��.
        Time.timeScale = 1;
        
        _manual.SetActive(false);
        _fieldGate.SetActive(false);

        //�� ����
        DataController.Instance.gameData._fieldGateOpen[0] = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
