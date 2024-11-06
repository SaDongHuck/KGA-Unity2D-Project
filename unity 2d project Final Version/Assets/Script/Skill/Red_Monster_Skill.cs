using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Monster_Skill: MonoBehaviour
{
    //public float attackSpeed = 1.0f; // 기본 공격 속도
    public float skillDuration = 10.0f; // 스킬 지속 시간
    public float increasedAttackSpeed = 1.3f; // 증가할 공격 속도 배율

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

        // 공격 속도 증가
        player.DamageSpeed *= increasedAttackSpeed;

        // 스킬 지속 시간 동안 대기
        yield return new WaitForSeconds(skillDuration);

        // 공격 속도를 원래대로 복구
        player.DamageSpeed = originalDamageSpeed;
        isSkillActive = false;
    }
}
