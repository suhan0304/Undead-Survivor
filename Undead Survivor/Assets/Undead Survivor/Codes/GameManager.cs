using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    public float gameTime;                  //게임 시간
    public float maxGameTime = 2 * 10f;     //게임 최대 시간 (test용 20초)

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;   //레벨
    public int kill;    //킬수
    public int exp;     //경험치
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
     

    void Awake()
    {
        //static은 인스턴스에 나오지 않으므로 초기화 해줘야함
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        //DeltaTime : 한 프레임에 걸린 시간
        gameTime += Time.deltaTime;

        //1초마다 Spawn 실행
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
