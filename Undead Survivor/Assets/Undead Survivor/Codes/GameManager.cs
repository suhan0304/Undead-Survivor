using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")]
    public bool isLive;                     //�ð� ���� ����
    public float gameTime;                  //���� �ð�
    public float maxGameTime = 2 * 10f;     //���� �ִ� �ð� (test�� 20��)

    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;   //����
    public int kill;    //ų��
    public int exp;     //����ġ
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp; //������ ���� ���� �� �ʱ�ȭ
    public Result uiResult;  //���â ������Ʈ
    public GameObject enemyCleaner;


    void Awake()
    {
        //static�� �ν��Ͻ��� ������ �����Ƿ� �ʱ�ȭ �������
        Instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0); //0��° ���� ��ư Click�̺�Ʈ ȣ��
        Resume();           //����� �� �ð� ����� 1�� ���� �ֵ��� start�� resume ȣ��
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false; //��� �۾� ����

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false; //��� �۾� ����

        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);//0��° Scene�� �ҷ��´�.
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
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive) // �������� ����ġ�� ������ ��Ȳ ����
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
        Time.timeScale = 0; //����Ƽ�� �ð� �ӵ�(����)
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; //����Ƽ�� �ð� �ӵ�(����)
    }
}
