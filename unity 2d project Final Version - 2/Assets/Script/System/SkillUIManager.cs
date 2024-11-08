using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SkillUIManager : MonoBehaviour
{
    public Player player; // 플레이어 스크립트 참조
    public Button[] enhanceButtons; // 각 스킬의 Enhance 버튼 배열
    //public Skill[] skills; // 스킬 배열
    [SerializeField] private TextMeshProUGUI[] coin;

    private void Start()
    {
        // 배열 길이가 일치하는지 확인
        if (enhanceButtons.Length != player.skills.Count)
        {
            Debug.LogError("Enhance 버튼 배열과 스킬 배열의 길이가 일치하지 않습니다!");
            return;
        }

        // 버튼 클릭 이벤트 설정
        for (int i = 0; i < enhanceButtons.Length; i++)
        {
            if (enhanceButtons[i] == null || player.skills[i] == null)
            {
                Debug.LogWarning($"Button 또는 Skill이 null입니다. Index: {i}");
                continue;
            }

            int index = i; // 지역 변수로 캡처
            UpdateCostText(index);

            enhanceButtons[i].onClick.AddListener(() => {
                print(player);
                if(player==null)
                {
                    player = GameManager.Instance.player;
                }
                OnEnhanceButtonClicked(player.skills[index], index);
            });
        }
    }

    public void OnEnhanceButtonClicked(Skill skill, int index)
    {
        if (skill == null)
        {
            Debug.LogWarning("Skill이 null입니다!");
            return;
        }

        if (player.coin >= skill.cost) // 코인이 충분한지 확인
        {
            player.OnSkillLevelUP(skill);
            //UpdateUI(); // UI 업데이트
            UpdateCostText(index);
        }
        else
        {
            Debug.Log("코인이 부족합니다!");
        }
    }
    private void UpdateCostText(int index)
    {
        if (coin[index] != null && player.skills[index] != null)
        {
            coin[index].text = "Enhance : " + player.skills[index].cost.ToString();
        }
    }
}
