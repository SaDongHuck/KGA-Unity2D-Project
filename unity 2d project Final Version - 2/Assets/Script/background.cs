using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    public Transform target;            // 배경이 초기 위치로 돌아갈 목표 위치
    public float scrollamount;          // 배경의 길이 (반복할 거리)
    public float moveSpeed;             // 배경 이동 속도
    public Vector3 moveDirection;       // 이동 방향
    public bool isScrolling = true;     // 스크롤링 상태를 저장하는 변수

    private void Start()
    {
        // target을 배경의 오른쪽 끝에 위치하도록 설정
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
