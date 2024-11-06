using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Monster_Skill : MonoBehaviour
{
    public bool isAtactive = false;

    public float sun_damage = 30f;

    private Enemy[] enemies;

    public void Use()
    {
        if(!isAtactive)
        {
            StartCoroutine(SuperAttackCorutine());
        }
    }

    private IEnumerator SuperAttackCorutine()
    {
        isAtactive=true;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.TakeDamage(sun_damage);
        }
        yield return new WaitForSeconds(1f);
        isAtactive=false;
    }
}
