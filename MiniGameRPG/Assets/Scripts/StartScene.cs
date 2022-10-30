using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press_StartButton()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void EraseData()
    {
        DataController.Instance.EraseGameData();

        SceneManager.LoadScene("VillageScene");
    }


    public void Press_Quit()
    {
        Application.Quit();
    }
}
