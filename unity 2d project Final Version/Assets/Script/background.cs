using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    public Transform target;            // ����� �ʱ� ��ġ�� ���ư� ��ǥ ��ġ
    public float scrollamount;          // ����� ���� (�ݺ��� �Ÿ�)
    public float moveSpeed;             // ��� �̵� �ӵ�
    public Vector3 moveDirection;       // �̵� ����
    public bool isScrolling = true;     // ��ũ�Ѹ� ���¸� �����ϴ� ����

    private void Start()
    {
        // target�� ����� ������ ���� ��ġ�ϵ��� ����
        target.position = transform.position + moveDirection * scrollamount;
    }

    private void Update()
    {
        if (isScrolling)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) >= scrollamount)
            {
                transform.position = target.position - moveDirection * scrollamount;
            }
        }
    }


}
