using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Monster_Skill : MonoBehaviour
{
    public bool isAttackActive = false;

    public float blue_damage = 10f;

    private Enemy[] enemies;
    public void Use()
    {
        if(!isAttackActive)
        {
            StartCoroutine(AttackCorutin());
        }
    }

    private IEnumerator AttackCorutin()
    {
        isAttackActive = true;
        enemies = FindObjectsOfType<Enemy>();
        Debug.Log($"찾은 적의 수: {enemies.Length}");
        foreach (var enemy in enemies)
        {
            Debug.Log($"적 이름: {enemy.name}에게 {blue_damage} 데미지");
            enemy.TakeDamage(blue_damage);
        }
        yield return new WaitForSeconds(1f);
        isAttackActive = false;
    }
}
