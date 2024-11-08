using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHP : MonoBehaviour
{
    public float HP_Bonus = 20;

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    private void Start()
    {
        player.hp += HP_Bonus;
    }
}
