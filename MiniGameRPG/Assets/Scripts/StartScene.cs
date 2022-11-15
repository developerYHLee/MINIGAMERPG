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
        
        //���������� ����
        string filePath = Application.persistentDataPath + "Savefile.json";
        FileInfo fileInfo = new FileInfo(filePath);

        //���� ���� ����
        if (!fileInfo.Exists)
        {
            //��ư ���� �Ұ�
            _loadData.GetComponent<Button>().enabled = false;

            //���� ����
            loadDataColor = _loadData.GetComponent<Image>().color;
            loadDataColor.a = 0.5f;
            _loadData.GetComponent<Image>().color = loadDataColor;
        }
        else
        {
            //��ư ���� ����
            _loadData.GetComponent<Button>().enabled = true;
            
            //���� ����
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
