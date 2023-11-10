using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    public bool isLive;                     //시간 정지 여부
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
    public LevelUp uiLevelUp; //레벨업 변수 선언 및 초기화
     

    void Awake()
    {
        //static은 인스턴스에 나오지 않으므로 초기화 해줘야함
        Instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0); //0번째 무기 버튼 Click이벤트 호출
        isLive = true;
    }

    void Update()
    {
        if (!isLive)
            return;

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
        Time.timeScale = 0; //유니티의 시간 속도(배율)
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; //유니티의 시간 속도(배율)
    }
}
