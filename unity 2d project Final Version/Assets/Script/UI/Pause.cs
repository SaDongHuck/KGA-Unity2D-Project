using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject panel;

    bool isPaused = false;

    public void Menu_button()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused?0:1; //게임 일시정지
        panel.SetActive(isPaused);
    }

    /*public void Continue()
    {
        Time.timeScale = 1;
        panel.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }*/

}
