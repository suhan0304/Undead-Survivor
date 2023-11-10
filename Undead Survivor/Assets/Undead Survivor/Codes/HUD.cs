using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //�ٷ�� �� �����͸� ������ enum���� ����
    public enum InfoType { Exp, Level, Kill, Time, Health }

    public InfoType type;

    //UnityEngine.UI ������ ���
    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();      //�ؽ�Ʈ   �ʱ�ȭ
        mySlider = GetComponent<Slider>();  //�����̴� �ʱ�ȭ
    }

    void LateUpdate() //������ ������ ���ŵǵ��� LateUpate ���
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.exp; //���� exp
                float maxExp = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.level, GameManager.Instance.nextExp.Length - 1)]; //�������� �ʿ��� ����ġ
                mySlider.value = curExp / maxExp;
                break;

            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill);
                break;
            case InfoType.Time:
                //�� �ð� - ���� ���� �ð� = �����ð�
                float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60); // �� = ���� �ð� (��) / 60 \ �Ҽ����� ������ int�� ��ȯ 
                int sec = Mathf.FloorToInt(remainTime % 60); //�� = ���� �ð� (��) % 60 \ �Ҽ����� ������ int�� ��ȯ
                myText.text = string.Format("{0:D2}:{1:D2}",min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.Instance.health; //���� exp
                float maxHealth = GameManager.Instance.maxHealth; //�������� �ʿ��� ����ġ
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
