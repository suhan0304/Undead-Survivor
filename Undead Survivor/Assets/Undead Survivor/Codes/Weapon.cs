using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public int id;          //무기 id
    public int prefabId;    //프리팹 id
    public float damage;    //데미지
    public int count;       //개수
    public float speed;     //속도

    public float timer;     //타이머
    Player player;          //플레이어가 저장될 변수

    void Awake()
    {
        player = GetComponentInParent<Player>(); //부모 오브젝트의 컴포넌트를 가져온다.
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        //무기 id에 맞게 로직 구현
        switch(id)
        {
            case 0:
                //z축 방향으로 back 방향으로 회전 (Speed가 음수라서 back 방향으로 지정)
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed) //speed 보다 커지면 초기화하면서 발사 로직 수행
                {
                    timer = 0; //타이머 초기화
                    Fire();     //발사하는 함수
                }
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(10,1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += 3;   //데미지 3 증가
        this.count += 1;    //카운트 1증가

        if (id == 0) //id가 0이면 재배치
            Batch();
    }


    public void Init()
    {

        //무기 id에 맞게 무기 속성을 설정
        switch(id)
        {
            case 0:
                speed = -150;   //마이너스 = 시계방향
                Batch();        //무기 배치          
                break;
            case 1:
                speed = 0.3f;   //0.3초에 한번 발사 
                break;
            default:
                break;
        }
    }

    void Batch()// 생성된 무기를 배치하는 함수
    {
        for(int i=0; i<count;i++)
        {
            //prefabId에 해당하는 prefab을 가져오면서 동시에 transform을 지역변수에 저장
            Transform bullet;
            // 기존 오브젝트를 먼저 활용하고 모자란 것은 풀링에서 가져오기
            if(i < transform.childCount) // 자식을 가지고 있으면 새로 꺼내지 않고
            {
                bullet = transform.GetChild(i);  //기존의 자식들을 가져다 쓴다.
            }
            else
            {
                bullet= GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;  //새로 가져오는 것들만 parent를 설정해주면 된다. 
                                            //- 기존 자식 오브젝트로 사용중이던 것은 이미 설정이 되어있음
            }

            bullet.localPosition = Vector3.zero; //bullet의 localPostion이 0으로 초기화 = 플레이어의 위치
            bullet.localRotation = Quaternion.identity; //Rotation값은 Quaternion형 값, 초기값은 identity

            Vector3 rotVec = Vector3.forward * 360 * i / count; //i번째 무기의 회전 각도를 계산
            bullet.Rotate(rotVec);                              //rotVec만큼 회전
            //Local 기준으로 방향이 위쪽으로 1.5만큼 이동 
            //이동 방향이 Space.self가 아니라 World인 이유는? 이미 회전 후 위쪽 방향으로 1.5만큼 이동시키는 것으로 했으므로 이동 방향은 월드를 기준으로 설정
            bullet.Translate(bullet.up * 1.5f, Space.World);    
            //Bullet의 Bullet 스크립트의 init 함수로 데미지 관통 초기화
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector2.zero); // -1 is Infinity Per. (근접공격은 무한 관통)
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) //nearestTarget이 없다면 return
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform. position; //크기가 포함된 방향 : 목표 위치 - 나의 위치
        dir = dir.normalized; //현재 벡터의 방향은 유지한체 크기만 1로 정규화 시켜준다.

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        //지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir); //관통을 count로 지정
    }
}
