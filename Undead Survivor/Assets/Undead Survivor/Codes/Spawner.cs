using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    //���͵��� �����Ͱ� �� �迭
    public SpawnData[] spawnData;

    int level; //�ð��� ���� ���� ����
    float timer;

    void Awake()
    {
        // �迭�� �������� ������ ���̱� ������ Component's'�� �����
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        //DeltaTime : �� �����ӿ� �ɸ� �ð�
        timer += Time.deltaTime;

        //gameTime�� 10���� ����� ����(Floor) �� �� ���������� ���� ����
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f), spawnData.Length - 1);


        //1�ʸ��� Spawn ���� 
        if (timer > spawnData[level].spawnTime) //level ���� ���� ���� �ֱ� ����
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        //0�� �Ǵ� 1�� ���͸� �������� ����
        GameObject enemy = GameManager.Instance.pool.Get(0); 

        //���� ����Ʈ���� �����ǵ��� ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //1������ �ϴ� ���� -> GetComponentsInChildren���� �ڱ� �ڽŵ� �����̶� 0���� �ڱ� �ڽ� transform�� ������

        //enemy�� ���� ������Ʈ�̱� ������ Enemy�� init�� �����ϱ� ���ؼ� Enemy�� GetComponent�� �����;���
        enemy.GetComponent<Enemy>().Init(spawnData[level]); //SpawnData ���� enemy�� �Ѱ���
    }
}


[System.Serializable] //������ Ŭ������ �ν����Ϳ��� �� �� �ֵ��� ����
public class SpawnData
{
    public float spawnTime; // ���� ��ȯ �ֱ�
    public int spriteType; // ������ ������ ��������Ʈ ����
    public int health; // ���� ü��
    public float speed; // ���� �ӵ�
}