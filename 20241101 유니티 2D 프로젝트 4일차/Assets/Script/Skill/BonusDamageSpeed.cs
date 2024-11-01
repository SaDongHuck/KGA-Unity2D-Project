using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDamageSpeed : MonoBehaviour
{
    public float Bonus_DamageSpeed = 3f;

    Player player;

    private void Awake()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
    }
    private void Start()
    {
        player.DamageSpeed += Bonus_DamageSpeed;
    }

}
