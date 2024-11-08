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
        Debug.Log($"ã�� ���� ��: {enemies.Length}");
        foreach (var enemy in enemies)
        {
            Debug.Log($"�� �̸�: {enemy.name}���� {blue_damage} ������");
            enemy.TakeDamage(blue_damage);
        }
        yield return new WaitForSeconds(1f);
        isAttackActive = false;
    }
}
