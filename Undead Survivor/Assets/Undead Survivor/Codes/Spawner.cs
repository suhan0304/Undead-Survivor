using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    void Awake()
    {
        // 배열로 여러개를 가져올 것이기 때문에 Component's'를 써야함
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        //DeltaTime : 한 프레임에 걸린 시간
        timer += Time.deltaTime;

        //1초마다 Spawn 실행
        if (timer > 1f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        //0번 또는 1번 몬스터를 랜덤으로 지정
        GameObject enemy = GameManager.Instance.pool.Get(Random.Range(0,2));

        //랜덤 포인트에서 생성되도록 설정
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //1번부터 하는 이유 -> GetComponentsInChildren에는 자기 자신도 포함이라 0번은 자기 자신 transform이 들어가있음
    }
}
