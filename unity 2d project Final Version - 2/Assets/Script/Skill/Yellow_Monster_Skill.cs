using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Monster_Skill : MonoBehaviour
{
    Player player;

    public float increaseHealAmount = 30f;

    public bool isHealActive = false;
    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();   
    }

    public void Use()
    {
        if(!isHealActive)
        {
            StartCoroutine(HealCorutine());
        }
    }

    private IEnumerator HealCorutine()
    {
        isHealActive=true;
        player.heal(increaseHealAmount);
        yield return new WaitForSeconds(5f);
        Debug.Log("�� �� �÷��̾� ���� ü��: " + player.hp);
        isHealActive =false;
    }

}
