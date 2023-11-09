using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft; //왼손인지 오른손인지 구분
    public SpriteRenderer spriter;

    SpriteRenderer player; //플레이어의 스프라이트 렌더러를 변수로 받아와 FlipX 여부를 확인

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0f);          // 오른손 위치
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0f);  // 반전 됐을때 오른손 위치

    Quaternion leftRot = Quaternion.Euler(0, 0, -35);       // 왼손의 회전을 수정하기 위해 Quaternion 형 사용
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);      // 반전 됐을때 회전


    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; 
        //부모의 spriteRenderer은 [1]번에 [0]은 자기 자신의 SpriteRenderer
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft) // 근접 무기
        {
            //항상 플레이어 기준으로 위치, 회전하기 때문에 local을 사용해야한다.
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse; //왼손 스프라이트(근접 무기)는 Y축 반전
            spriter.sortingOrder = isReverse ? 4 : 6; //반전 여부에 따라 order Layer 설정
        }
        else // 원거리 무기
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;  //오른손 스프라이트(원거리 무기)는 X축 반전
            spriter.sortingOrder = isReverse ? 6 : 4; //반전 여부에 따라 order Layer 설정
        }
    }
}
