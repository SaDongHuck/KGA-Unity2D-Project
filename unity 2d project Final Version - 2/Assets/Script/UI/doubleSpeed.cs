using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class doubleSpeed : MonoBehaviour
{
    private bool isDoubleSpeed = false;  // 현재 게임 속도가 두 배인지 여부를 저장

    public void ToggleGameSpeed()
    {
        if (isDoubleSpeed)
        {
            Time.timeScale = 1f;  // 게임 속도를 원래대로
            isDoubleSpeed = false;
        }
        else
        {
            Time.timeScale = 2f;  // 게임 속도를 두 배로
            isDoubleSpeed = true;
        }
    }
}
