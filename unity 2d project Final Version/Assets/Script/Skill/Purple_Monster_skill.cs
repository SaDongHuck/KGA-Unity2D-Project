using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Monster_skill : MonoBehaviour
{
    public float defenseBuff = 10f;  // 방어력 증가 스킬 사용 시 증가할 값
    public bool isDefenseBuffActive = false;

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    public void use()
    {
        if (!isDefenseBuffActive)
        {
            StartCoroutine(DefenseBuffCoroutine());
        }
    }

    private IEnumerator DefenseBuffCoroutine()
    {
        isDefenseBuffActive = true;
        player.defense += defenseBuff;
        yield return new WaitForSeconds(5f);
        player.defense -= defenseBuff;
        isDefenseBuffActive = false;

    }
}
