using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamage : BonusCritical
{
    public float criticalMultiplier = 2f;

    Player player;

    BonusCritical critical;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    private void Start()
    {
        if(Random.value < critical.criticalChance)
        {
            player.Dmage *= criticalMultiplier;
        }
    }
}
