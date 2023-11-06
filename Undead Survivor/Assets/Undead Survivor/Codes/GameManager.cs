using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime;                  //���� �ð�
    public float maxGameTime = 2 * 10f;     //���� �ִ� �ð� (test�� 20��)

    public PoolManager pool;
    public Player player;


    void Awake()
    {
        //static�� �ν��Ͻ��� ������ �����Ƿ� �ʱ�ȭ �������
        Instance = this;
    }

    void Update()
    {
        //DeltaTime : �� �����ӿ� �ɸ� �ð�
        gameTime += Time.deltaTime;

        //1�ʸ��� Spawn ����
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

}
