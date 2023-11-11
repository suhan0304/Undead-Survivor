using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;      //잠금된 캐릭터 버튼
    public GameObject[] unlockCharacter;    //해제된 캐릭터 버튼

    enum Achive { UnlockPotato, UnlockBean }

    Achive[] achives;

    void Awake()
    {
        //Achive 열거형을 achives 리스트에 저장
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        if (!PlayerPrefs.HasKey("MyData")) // MyData가 없으면 처음 실행을 의미
        {
            Init(); 
        }
    }

    void Init()
    {
        //PlayerPrefs는 유니티에서 제공하는 저장 기능

        PlayerPrefs.SetInt("MyData", 1); //MyData라는 키에 1을 저장

        //모든 업적을 초기화
        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for(int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();//업적 이름 가져오기
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1; //업적 이름에 해당하는 데이터 가져오기

            lockCharacter[i].SetActive(!isUnlock); //해금이 된 잠금 버튼은 비활성화
            unlockCharacter[i].SetActive(isUnlock); //해금이 된 캐릭터 버튼은 활성화
        }
    }
    
    void LateUpdate()
    {
        foreach (Achive achive in achives)
        { //업적 배열안에 있는 업적들 확인
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive) //업적 달성을 위한 함수
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato://킬수가 10 이상이면
                isAchive = GameManager.Instance.kill >= 10;
                break;

            case Achive.UnlockBean: // 게임시간이 최대까지 버티면 (생존)
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
        }
        //업적 달성 + 해금이 안된 상태이면 해금 진행
        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);//업적 해금
        }
    }
}
