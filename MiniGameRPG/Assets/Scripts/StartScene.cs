using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartScene : MonoBehaviour
{
    public GameObject _loadData;

    // Start is called before the first frame update
    void Start()
    {
        Color loadDataColor;
        
        //데이터파일 정보
        string filePath = Application.persistentDataPath + "Savefile.json";
        FileInfo fileInfo = new FileInfo(filePath);

        //파일 존재 유무
        if (!fileInfo.Exists)
        {
            //버튼 동작 불가
            _loadData.GetComponent<Button>().enabled = false;

            //투명도 설정
            loadDataColor = _loadData.GetComponent<Image>().color;
            loadDataColor.a = 0.5f;
            _loadData.GetComponent<Image>().color = loadDataColor;
        }
        else
        {
            //버튼 동작 가능
            _loadData.GetComponent<Button>().enabled = true;
            
            //투명도 설정
            loadDataColor = _loadData.GetComponent<Image>().color;
            loadDataColor.a = 1;
            _loadData.GetComponent<Image>().color = loadDataColor;
        }
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
