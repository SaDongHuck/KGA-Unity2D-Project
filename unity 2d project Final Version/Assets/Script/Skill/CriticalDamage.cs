using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamage : MonoBehaviour
{
    public float criticalMultiplier = 2f;

    public float criticalChance = 0.2f;

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }

    private void Start()
    {
        if(Random.value < criticalChance)
        {
            player.Dmage *= criticalMultiplier;
        }
    }
}
