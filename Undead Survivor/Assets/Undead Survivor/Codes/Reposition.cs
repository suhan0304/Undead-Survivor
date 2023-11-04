using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    //Trigger가 check된 collider에서 나갔을때 호출
    void OnTriggerExit2D(Collider2D collision)
    {

        if(!collision.CompareTag("Area")) //나간 Collision의 Tag가 Area가 아니면 바로 return
            return;


        //Player의 위치를 가져옴
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        //내 위치를 가져옴
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x); //x축 좌표 차이
        float diffY = Mathf.Abs(playerPos.y - myPos.y); //y축 좌표 차이

        //플레이어의 방향을 파악
        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1; //player의 inputVec의 x가 음수이다? 진행방이 왼쪽(-1), 아닐경우 오른쪽(1)
        float dirY = playerDir.y < 0 ? -1 : 1; //player의 inputVec의 y가 음수이다? 진행방이 아래쪽(-1), 아닐경우 위쪽(1)

        //collsion이 어떤 태그이냐에 따라 동작이 다름
        //추후에 몬스터도 재배치를 해주기 위해 설정
        switch (transform.tag)
        {
            case "Ground": //collsion의 태그가 ground일 경우
                if (diffX > diffY) //두 오브젝트의 거리 차이에서 X축이 Y축보다 크면 맵을 수평이동
                {
                    //Translate 지정된 값 만큼 현재 위치에서 이동
                    transform.Translate(Vector3.right * dirX * 40);  //오른쪽 단위 벡터(1, 0, 0) * 방향(왼쪽 -1 , 오른쪽 1) * 크기(40) 
                                                                     //크기가 40인 이유는 타일맵을 4개를 사용해서 2*2로 설정했기 때문
                }
                else if (diffX < diffY)  //두 오브젝트의 거리 차이에서 Y축이 X축보다 크면 맵을 수평이동
                {
                    //Translate 지정된 값 만큼 현재 위치에서 이동
                    transform.Translate(Vector3.up * dirY * 40); 
                }
                else
                {
                    transform.Translate(dirX * 40, dirY * 40, 0);
                }
                break;

            case "Enemy": //collsion의 태그가 Enemy일 경우
                if (coll.enabled) // 콜라이더가 활성화 되어있는지 조건 먼저 작성 - 몬스터가 죽었으면 비활성화 되어있음 => 재배치 필요가 없음
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f)); 
                    //적어도 맵 크기만큼은 이동 -> 플레이어의 시야 밖에서 재배치 되도록 
                }
                break;
        }


    } 
}
