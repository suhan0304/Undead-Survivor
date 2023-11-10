using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    public bool isLive;                     //�ð� ���� ����
    public float gameTime;                  //���� �ð�
    public float maxGameTime = 2 * 10f;     //���� �ִ� �ð� (test�� 20��)

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;   //����
    public int kill;    //ų��
    public int exp;     //����ġ
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp; //������ ���� ���� �� �ʱ�ȭ
     

    void Awake()
    {
        //static�� �ν��Ͻ��� ������ �����Ƿ� �ʱ�ȭ �������
        Instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0); //0��° ���� ��ư Click�̺�Ʈ ȣ��
        isLive = true;
    }

    void Update()
    {
        if (!isLive)
            return;

        //DeltaTime : �� �����ӿ� �ɸ� �ð�
        gameTime += Time.deltaTime;

        //1�ʸ��� Spawn ����
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; //����Ƽ�� �ð� �ӵ�(����)
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; //����Ƽ�� �ð� �ӵ�(����)
    }
}
