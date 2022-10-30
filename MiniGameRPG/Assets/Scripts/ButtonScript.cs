using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject function_background;
    public GameObject[] _uiList;

    // Start is called before the first frame update
    void Start()
    {

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

    public void ToggleEquipmentList()
    {
        _uiList[1].SetActive(!_uiList[1].activeSelf);
    }

    public void ToggleInventoryList()
    {
        _uiList[2].SetActive(!_uiList[2].activeSelf);
    }
}