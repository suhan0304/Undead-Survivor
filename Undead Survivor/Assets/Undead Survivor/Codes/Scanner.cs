using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;         //스캔 범위를 지정
    public LayerMask targetLayer;   //스캔할 레이어
    public RaycastHit2D[] targets;  //스캔 결과 배열
    public Transform nearestTarget; //가장 가까운 목표
    
    void FixedUpdate()
    {
        // 원형의 캐스트를 쏘고 모든 결과를 반환하는 함수 
        //우리의 목적? 플레이어를 중심으로 원으로 객체를 탐색
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        //CircleCastAll     (1.캐스팅 시작위치, 
        //                   2. 원의 반지름, 
        //                   3. 캐스팅 방향,  => 지금 우리는 특정 방향으로 캐스트를 쏘는 것이 목적이 아니므로Vector2.zero 사용
        //                   4. 캐스팅 길이,  => 캐스팅을 쏘는 것이 목적이 아니라 길이가 0 
        //                   5. 대상 레이어) 
        nearestTarget = GetNearest();
    }

    //가장 가까운 오브젝트의 Transform을 반환 시켜줄 함수
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100; //플레이어와의 거리

        //foreach 문으로 캐스팅 결와 오브젝트를 하나씩 방문
        foreach (RaycastHit2D target in targets)
        {  //CircleCastAll에 맞은 애들을 하나씩 접근
            Vector3 myPos = transform.position;             //플레이어 위치
            Vector3 targetPos = target.transform.position;  //타겟의 위치
            float curDiff = Vector3.Distance(myPos, targetPos); //거리를 구한다.

            if (curDiff < diff) //최소 거리를 저장
            {
                diff = curDiff;
                result = target.transform; //결과는 가장 가까운 타겟의 transform으로 지정
            }

        }

        return result;
    }
}
