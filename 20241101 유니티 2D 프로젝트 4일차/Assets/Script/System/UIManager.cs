using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : SingletonManager<UIManager>
{
    public Text coin;

    public Text stage;

    public Image hpBar;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        coin.text = GameManager.Instance.player.coin.ToString();
        hpBar.fillAmount = GameManager.Instance.player.HpAmount;
        stage.text = "STAGE : " + GameManager.Instance.currentStage.ToString(); 
        
    }
}
