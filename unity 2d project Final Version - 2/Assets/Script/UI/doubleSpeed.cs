using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class doubleSpeed : MonoBehaviour
{
    private bool isDoubleSpeed = false;  // ���� ���� �ӵ��� �� ������ ���θ� ����

    public void ToggleGameSpeed()
    {
        if (isDoubleSpeed)
        {
            Time.timeScale = 1f;  // ���� �ӵ��� �������
            isDoubleSpeed = false;
        }
        else
        {
            Time.timeScale = 2f;  // ���� �ӵ��� �� ���
            isDoubleSpeed = true;
        }
    }
}
