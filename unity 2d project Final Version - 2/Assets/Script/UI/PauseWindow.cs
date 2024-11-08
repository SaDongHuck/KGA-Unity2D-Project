using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    public GameObject windowpanel;

    bool isPaused = false;
    public void Continue()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused?0:1;
        windowpanel.SetActive(isPaused);
    }

    public void GameExit()
    {
        //Application.Quit();
     #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
     #else
        Application.Quit();
     #endif
    }

}
