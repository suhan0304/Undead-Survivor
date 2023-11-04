using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    void Awake()
    {
        // �迭�� �������� ������ ���̱� ������ Component's'�� �����
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        //DeltaTime : �� �����ӿ� �ɸ� �ð�
        timer += Time.deltaTime;

        //1�ʸ��� Spawn ����
        if (timer > 1f)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        //0�� �Ǵ� 1�� ���͸� �������� ����
        GameObject enemy = GameManager.Instance.pool.Get(Random.Range(0,2));

        //���� ����Ʈ���� �����ǵ��� ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //1������ �ϴ� ���� -> GetComponentsInChildren���� �ڱ� �ڽŵ� �����̶� 0���� �ڱ� �ڽ� transform�� ������
    }
}
