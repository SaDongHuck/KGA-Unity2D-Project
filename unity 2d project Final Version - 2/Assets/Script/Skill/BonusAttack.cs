using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAttack : MonoBehaviour
{
    public float Bonus_attack = 2;

    Player player;
    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();    
    }
    void Start()
    {
        player.Dmage += Bonus_attack;
    }

}
