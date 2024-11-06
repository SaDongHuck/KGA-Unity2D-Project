using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Monster_Skill: MonoBehaviour
{
    //public float attackSpeed = 1.0f; // �⺻ ���� �ӵ�
    public float skillDuration = 10.0f; // ��ų ���� �ð�
    public float increasedAttackSpeed = 1.3f; // ������ ���� �ӵ� ����

    private bool isSkillActive = false;
    private float originalDamageSpeed;

    Player player;

    private void Activate()
    {
        player = GameManager.Instance.player;
        originalDamageSpeed = player.DamageSpeed;
    }
    public void Use()
    {
        if (!isSkillActive)
        {
            StartCoroutine(ActivateSkill());
        }
    }
    public void Inactivate()
    {
        player.DamageSpeed = originalDamageSpeed;
    }
    IEnumerator ActivateSkill()
    {
        isSkillActive = true;

        // ���� �ӵ� ����
        player.DamageSpeed *= increasedAttackSpeed;

        // ��ų ���� �ð� ���� ���
        yield return new WaitForSeconds(skillDuration);

        // ���� �ӵ��� ������� ����
        player.DamageSpeed = originalDamageSpeed;
        isSkillActive = false;
    }
}
