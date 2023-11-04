using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;         //속도
    public Rigidbody2D target;  //목표 Rigidbody

    bool isLive = true; //생존여부

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        if (!isLive) //죽었으면 종료
            return;

        Vector2 dirVec = target.position - rigid.position; // 방향 = 위치 차이의 정규화 (위치 차이 = 타겟 위치 - 나의 위치)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //방향(정규화) * 속도 * 프레임 시간 보정
        rigid.MovePosition(rigid.position + nextVec); //현재 위치에 next벡터를 더한다.
        //다른 리지드바디와 부딪히게 되면 물리 속도가 생기는데 우리는 위치 이동을 채용하고 있으므로 물리 속도로 위치가 변화하면 안되므로 velocity를 0 만들기
        rigid.velocity = Vector2.zero; //물리 속도가 이동에 영향을 주지 않도록 속도는 제거 
    }

    private void LateUpdate()
    {
        if (!isLive) //죽었으면 종료
            return;
 
        //목표의 x축과 자신의 x축 값을 비교하여 작으면 X축을 기준으로 Flip 되도록 FlipX를 True로 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }

}
