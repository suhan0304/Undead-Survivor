using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    //몬스터들의 데이터가 들어갈 배열
    public SpawnData[] spawnData;
    public float levelTime;

    int level; //시간에 따른 레벨 지정
    float timer;

    void Awake()
    {
        // 배열로 여러개를 가져올 것이기 때문에 Component's'를 써야함
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.Instance.maxGameTime / spawnData.Length;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        //DeltaTime : 한 프레임에 걸린 시간
        timer += Time.deltaTime;

        //gameTime을 levelTime으로 나누어서 내림(Floor) 한 후 정수형으로 레벨 지정
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / levelTime), spawnData.Length - 1);


        //1초마다 Spawn 실행 
        if (timer > spawnData[level].spawnTime) //level 값에 따라 스폰 주기 변경
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        //0번 또는 1번 몬스터를 랜덤으로 지정
        GameObject enemy = GameManager.Instance.pool.Get(0); 

        //랜덤 포인트에서 생성되도록 설정
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //1번부터 하는 이유 -> GetComponentsInChildren에는 자기 자신도 포함이라 0번은 자기 자신 transform이 들어가있음

        //enemy는 게임 오브젝트이기 때문에 Enemy의 init을 실행하기 위해선 Enemy를 GetComponent로 가져와야함
        enemy.GetComponent<Enemy>().Init(spawnData[level]); //SpawnData 값을 enemy에 넘겨줌
    }
}


[System.Serializable] //복잡한 클래스를 인스펙터에서 볼 수 있도록 설정
public class SpawnData
{
    public float spawnTime; // 몬스터 소환 주기
    public int spriteType; // 등장할 몬스터의 스프라이트 종류
    public int health; // 몬스터 체력
    public float speed; // 몬스터 속도
}