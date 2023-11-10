using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //다루게 될 데이터를 열겨형 enum으로 선언
    public enum InfoType { Exp, Level, Kill, Time, Health }

    public InfoType type;

    //UnityEngine.UI 선언후 사용
    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();      //텍스트   초기화
        mySlider = GetComponent<Slider>();  //슬라이더 초기화
    }

    void LateUpdate() //연산이 끝나고 갱신되도록 LateUpate 사용
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.exp; //현재 exp
                float maxExp = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.level, GameManager.Instance.nextExp.Length - 1)]; //레벨업에 필요한 경험치
                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break;
            case InfoType.Time:
                //총 시간 - 현재 게임 시간 = 남은시간
                float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60); // 분 = 남은 시간 (초) / 60 \ 소숫점은 버리고 int로 변환 
                int sec = Mathf.FloorToInt(remainTime % 60); //분 = 남은 시간 (초) % 60 \ 소숫점은 버리고 int로 변환
                myText.text = string.Format("{0:D2}:{1:D2}",min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.Instance.health; //현재 exp
                float maxHealth = GameManager.Instance.maxHealth; //레벨업에 필요한 경험치
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
