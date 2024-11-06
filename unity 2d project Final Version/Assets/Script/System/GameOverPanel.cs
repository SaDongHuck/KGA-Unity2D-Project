using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public GameObject gamepaenl;

    bool isPaused = false;
    public void RestartGame()
    {
        isPaused = !isPaused;
        Time.timeScale =isPaused?0:1f; // 게임 재개
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
        gamepaenl.SetActive(isPaused);
    }

}
