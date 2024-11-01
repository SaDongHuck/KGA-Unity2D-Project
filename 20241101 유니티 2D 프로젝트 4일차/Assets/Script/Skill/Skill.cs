using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
   public int cost = 20;
   public int costIncrease = 30;
   public string SkillName; //스킬 이름
   public int SkillLevel; // 스킬 레벨
   public bool isTargeting; //가장 가까운 적을 향하는 스킬인지
   public GameObject skillprefab; // 스킬 프리팹
   public GameObject[] skillprefabs;
   public GameObject currentSkillObject;

    public void OnLevelup()
    {
        //if(레벨업 가능한지)
        SkillLevel++;
        GameManager.Instance.player.coin -= cost;
        cost += costIncrease;
    }
}
