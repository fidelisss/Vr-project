using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject _mainMenu, _lectureSelectPanel;


    private void Awake()
    {
        _mainMenu.SetActive(true);
        _lectureSelectPanel.SetActive(false);
    }
    public void SelectLecture()
    {
        _mainMenu.SetActive(false);
        _lectureSelectPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        _mainMenu.SetActive(true);
        _lectureSelectPanel.SetActive(false);
    }


}
