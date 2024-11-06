using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonManager<UIManager>
{
    public Text coin;

    public Text stage;

    public Image hpBar;

    public Text AttackPower;

    public GameObject gameOverUI;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if(hpBar == null)
        {
            hpBar = GameManager.Instance.player.hpBar;
        }
        coin.text = GameManager.Instance.player.coin.ToString();
        hpBar.fillAmount = GameManager.Instance.player.HpAmount;
        stage.text = "STAGE : " + GameManager.Instance.currentStage.ToString(); 
        AttackPower.text = GameManager.Instance.player.attackpower.ToString();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
