using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;      //��ݵ� ĳ���� ��ư
    public GameObject[] unlockCharacter;    //������ ĳ���� ��ư
    public GameObject uiNotice;

    enum Achive { UnlockPotato, UnlockBean }
    Achive[] achives;
    WaitForSecondsRealtime wait; //������ �ʴ½ð�

    void Awake()
    {
        //Achive �������� achives ����Ʈ�� ����
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData")) // MyData�� ������ ó�� ������ �ǹ�
        {
            Init(); 
        }
    }

    void Init()
    {
        //PlayerPrefs�� ����Ƽ���� �����ϴ� ���� ���

        PlayerPrefs.SetInt("MyData", 1); //MyData��� Ű�� 1�� ����

        //��� ������ �ʱ�ȭ
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
            string achiveName = achives[i].ToString();//���� �̸� ��������
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1; //���� �̸��� �ش��ϴ� ������ ��������

            lockCharacter[i].SetActive(!isUnlock); //�ر��� �� ��� ��ư�� ��Ȱ��ȭ
            unlockCharacter[i].SetActive(isUnlock); //�ر��� �� ĳ���� ��ư�� Ȱ��ȭ
        }
    }
    
    void LateUpdate()
    {
        foreach (Achive achive in achives)
        { //���� �迭�ȿ� �ִ� ������ Ȯ��
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive) //���� �޼��� ���� �Լ�
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato://ų���� 10 �̻��̸�
                isAchive = GameManager.Instance.kill >= 10;
                break;

            case Achive.UnlockBean: // ���ӽð��� �ִ���� ��Ƽ�� (����)
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
        }
        //���� �޼� + �ر��� �ȵ� �����̸� �ر� ����
        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);//���� �ر�

            //� �������� Notice�� ��Ÿ���ִ� ����
            for(int i=0;i<uiNotice.transform.childCount;i++)
            {
                bool isActive = i == (int)achive; //ù��° ��������, �ι�° �������� Ȯ��
                uiNotice.transform.GetChild(i).gameObject.SetActive(isActive); //i��° �ڽ� Ȱ��ȭ
            }

            //Notice Ȱ��ȭ
            StartCoroutine(NoticeRoutine());
        }
    }

    //�˸� â�� Ȱ��ȭ�ߴٰ� ���� �ð� ���� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ����
    IEnumerator NoticeRoutine()
    {
        //Notice Ȱ��ȭ
        uiNotice.SetActive(true);

        yield return wait; //5�� ��� (���� �̸� �����ؼ� �޸� ����)

        //Notice ��Ȱ��ȭ
        uiNotice.SetActive(false);
    }
}
