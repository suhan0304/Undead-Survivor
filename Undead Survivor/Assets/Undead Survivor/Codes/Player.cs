using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); //true를 넣으면 비활성화 된 오브젝트의 컴포넌트도 가져올 수 있다.
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];    
    }


    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) //살아있을때만 실행
            return;

        // 프레임마다 이벤트가 실행되기 때문에 DeltaTime을 곱해주어야 한다.
        GameManager.Instance.health -= Time.deltaTime * 10;

        if(GameManager.Instance.health < 0)
        {
            for(int i=2;i<transform.childCount;i++) //자식 오브젝트들을 비활성화
            {
                //Spawner부터 Hand 모두 비활성화
                transform.GetChild(i).gameObject.SetActive(false); 
                //자식의 transform이 반환되므로 다시 gameobject로 올라가서 비활성화
            }

            //죽음 애니메이션 실행
            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
}
