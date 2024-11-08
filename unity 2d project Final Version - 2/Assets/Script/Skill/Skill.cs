using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
   public int cost = 20;
   public int costIncrease = 30;
   public string SkillName; //��ų �̸�
   public int SkillLevel; // ��ų ����
   public bool isTargeting; //���� ����� ���� ���ϴ� ��ų����
   public GameObject skillprefab; // ��ų ������
   public GameObject[] skillprefabs;
   public GameObject currentSkillObject;

    public void OnLevelup()
    {
        //if(������ ��������)
        SkillLevel++;
        GameManager.Instance.player.coin -= cost;
        cost += costIncrease;
    }
}
