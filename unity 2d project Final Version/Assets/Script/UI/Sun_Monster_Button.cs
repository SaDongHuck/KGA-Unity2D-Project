using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sun_Monster_Button : MonoBehaviour
{
    public float cooldown = 5f;

    private float useTime;

    public Button SkillButton;

    public Image cooldownIndiCator;

    public int skillID;

    public float attackSpeedMultiplier = 1.3f;
    private void Start()
    {
        useTime = -cooldown;
    }
    private void Update()
    {
        float cooldownAmount = (Time.time - useTime) / cooldown;
        cooldownIndiCator.fillAmount = 1 - cooldownAmount;
    }

    public void Use()
    {
        if (Time.time - useTime < cooldown)
        {
            print("�ð��� ������� �ʴ�");

            return;
        }
        print("��ų ���");
        GameManager.Instance.player.UseSkill(skillID);
        useTime = Time.time;
    }
}
