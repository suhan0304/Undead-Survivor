using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;         //속도
    public float health;        //체력
    public float maxHealth;     //최대 체력
    public RuntimeAnimatorController[] animCon;     //몬스터의 애니메이터를 바꾸기 위한 컨트롤러
    public Rigidbody2D target;  //목표 Rigidbody

    bool isLive; //생존여부

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    
    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) //죽었으면 종료
            return;

        Vector2 dirVec = target.position - rigid.position; // 방향 = 위치 차이의 정규화 (위치 차이 = 타겟 위치 - 나의 위치)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //방향(정규화) * 속도 * 프레임 시간 보정
        rigid.MovePosition(rigid.position + nextVec); //현재 위치에 next벡터를 더한다.
        //다른 리지드바디와 부딪히게 되면 물리 속도가 생기는데 우리는 위치 이동을 채용하고 있으므로 물리 속도로 위치가 변화하면 안되므로 velocity를 0 만들기
        rigid.velocity = Vector2.zero; //물리 속도가 이동에 영향을 주지 않도록 속도는 제거 
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive) //죽었으면 종료
            return;
 
        //목표의 x축과 자신의 x축 값을 비교하여 작으면 X축을 기준으로 Flip 되도록 FlipX를 True로 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() //활성화 될 때 한 번 실행
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true; //생존여부 초기화
        // Dead 오브젝트를 다시 초기화
        coll.enabled = true;    
        rigid.simulated = true;    
        spriter.sortingOrder = 2;   
        anim.SetBool("Dead", false);  
        health = maxHealth;
    }

    public void Init(SpawnData data) //초기 속성을 적용하는 함수 작성
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];  //애니메이션 적용
        speed = data.speed;         //속도 적용
        maxHealth = data.health;    //체력 적용
        health = data.health;                                           
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // 충돌한 collision이 Bullet인지를 먼저 확인
            return;

        health -= collision.GetComponent<Bullet>().damage; //Bullet 스크립트 컴포넌트에서 damage를 가져와서 체력에서 깍는다.
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // .. 아직 살아있음 -> Hit Action 
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            // .. 체력이 0보다 작음 -> Die 
            // 아래에서 설정한 변수들은 재활용에서 또 다시 사용할 때를 위해 init에 초기화 코드를 추가 작성
            coll.enabled = false;       //충돌 방지를 위해 collider 2D 비활성화
            rigid.simulated = false;    //물리적 활동 방지를 위해 rigidbody 2D 비활성화
            spriter.sortingOrder = 1;   //활동중이 몬스터들을 가리지 않도록 sortingLayer을 낮춘다
            anim.SetBool("Dead", true);  //Dead 애니메이션을 실행 
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
            if(GameManager.Instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);

        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임을 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position; //Player의 Postion
        Vector3 dirVec = transform.position - playerPos;                    //Player에서 Enemy로의 방향(=넉백 방향)
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); //순간적인 힘이므로 ForceMode2D.Impulse 실행
    }

    void Dead()
    {
        //비활성화를 해준다.
        gameObject.SetActive(false);
    }
}
