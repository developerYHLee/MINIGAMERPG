using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu_background, resume_background, option_background;

    // Start is called before the first frame update
    void Start()
    {
        option_background.SetActive(false);
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

    public void Quit()
    {
        Application.Quit();
    }
}
