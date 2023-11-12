using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    public bool isLive;                     //시간 정지 여부
    public float gameTime;                  //게임 시간
    public float maxGameTime = 2 * 10f;     //게임 최대 시간 (test용 20초)

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;   //레벨
    public int kill;    //킬수
    public int exp;     //경험치
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp; //레벨업 변수 선언 및 초기화
    public Result uiResult;   //결과창 오브젝트
    public Transform uiJoy;   //조이스틱 오브젝트
    public GameObject enemyCleaner;
    

    void Awake()
    {
        //static은 인스턴스에 나오지 않으므로 초기화 해줘야함
        Instance = this;
        Application.targetFrameRate = 60;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2); //고른 캐릭터에 따라 기본무기 변경
        Resume();           //재시작 시 시간 배속을 1로 맞춰 주도록 start에 resume 호출

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false; //모든 작업 정지

        yield return new WaitForSeconds(0.5f);
        
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false; //모든 작업 정지

        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);//0번째 Scene을 불러온다.
    }

    public void GameQuit()
    {
        Application.Quit();
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
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive) // 끝났을때 경험치가 오르는 상황 방지
            return; 

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
        uiJoy.localScale = Vector3.zero; //조이스틱 숨기기
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; //유니티의 시간 속도(배율)
        uiJoy.localScale = Vector3.one; //조이스틱 보여주기
    }
}
