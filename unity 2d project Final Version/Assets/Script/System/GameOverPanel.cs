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
        Time.timeScale =isPaused?0:1f; // ���� �簳
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� �� �ٽ� �ε�
        gamepaenl.SetActive(isPaused);
    }

}
