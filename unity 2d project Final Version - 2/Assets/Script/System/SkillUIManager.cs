using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SkillUIManager : MonoBehaviour
{
    public Player player; // �÷��̾� ��ũ��Ʈ ����
    public Button[] enhanceButtons; // �� ��ų�� Enhance ��ư �迭
    //public Skill[] skills; // ��ų �迭
    [SerializeField] private TextMeshProUGUI[] coin;

    private void Start()
    {
        // �迭 ���̰� ��ġ�ϴ��� Ȯ��
        if (enhanceButtons.Length != player.skills.Count)
        {
            Debug.LogError("Enhance ��ư �迭�� ��ų �迭�� ���̰� ��ġ���� �ʽ��ϴ�!");
            return;
        }

        // ��ư Ŭ�� �̺�Ʈ ����
        for (int i = 0; i < enhanceButtons.Length; i++)
        {
            if (enhanceButtons[i] == null || player.skills[i] == null)
            {
                Debug.LogWarning($"Button �Ǵ� Skill�� null�Դϴ�. Index: {i}");
                continue;
            }

            int index = i; // ���� ������ ĸó
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
            Debug.LogWarning("Skill�� null�Դϴ�!");
            return;
        }

        if (player.coin >= skill.cost) // ������ ������� Ȯ��
        {
            player.OnSkillLevelUP(skill);
            //UpdateUI(); // UI ������Ʈ
            UpdateCostText(index);
        }
        else
        {
            Debug.Log("������ �����մϴ�!");
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
