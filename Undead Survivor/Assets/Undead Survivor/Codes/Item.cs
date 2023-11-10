using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;   //������ ������
    public int level;       //���� 
    public Weapon weapon;   //���� 
    public Gear gear;       //��� 

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        //�ڽ� ������Ʈ icon
        icon = GetComponentsInChildren<Image>()[1]; //ù��°[0]�� �ڱ��ڽ�
        icon.sprite = data.itemIcon;                //itemData�� ���������� �ʱ�ȭ

        Text[] texts = GetComponentsInChildren<Text>(); //�ڽ��� Text ������Ʈ ��������
        textLevel = texts[0];   //item ������Ʈ���� Text�� ��� �ڽĿ� �ִ� Text�� ���� ������ ù��°[0]�� �ʱ�ȭ
        textName = texts[1];    //GetComponents�� ������ ���� ������ ������ ���󰣴�.
        textDesc = texts[2];
        textName.text = data.itemName; //������ �̸� �����ε� ��ư�� �����̹Ƿ� �ٷ� �ʱ�ȭ
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                //����� �Էµ� itemDesc�� �ؽ�Ʈ�� �Ű������� damages�� counts�� �־ ���ڿ� ����
                textDesc.text = string.Format(data.itemDesc, data.damages[level], data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                //���� �Էµ� itemDesc�� �ؽ�Ʈ�� �Ű������� damages�� counts�� �־ ���ڿ� ����
                textDesc.text = string.Format(data.itemDesc, data.damages[level]);
                break;
        }
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee: // ����, ���Ÿ� ����� ���� ������ ��� 
            case ItemData.ItemType.Range: // case�� �ٿ��ش�.
                if (level == 0) //������ 0�� �� ��ư�� ������ ���� ������Ʈ�� ����
                {
                    GameObject newWeapon = new GameObject();

                    //���ο� ������Ʈ�� Weapon ������Ʈ �߰�
                    //AddComponent �Լ� ��ȯ ���� �̸� ������ ������ ����.
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);

                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level]; //damages�� ������̱� ������ ���ؼ� ������
                    nextCount += data.counts[level]; //count�� �ܼ��� counts�� ������ �ε������ؼ� ������ ���� ������

                    weapon.LevelUp(nextDamage, nextCount); //Weapon�� LevelUp �Լ��� �̿��� ������
                }

                level++;
                break;
            case ItemData.ItemType.Glove: // ���Ⱑ �ƴ� ������ ���� ������ ���
            case ItemData.ItemType.Shoe:
                if (level == 0) //������ 0�� �� ��ư�� ������ ��� ������Ʈ�� ����
                {
                    GameObject newGear = new GameObject();

                    //���ο� ������Ʈ�� Weapon ������Ʈ �߰�
                    //AddComponent �Լ� ��ȯ ���� �̸� ������ ������ ����.
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];

                    gear.LevelUp(nextRate);
                }

                level++;
                break;
            case ItemData.ItemType.Heal: // ��ȸ�� �������� �ȿ����� LevelUp �Լ� ������ X
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                break;
        }
        // ��ũ��Ʈ�� ������Ʈ�� �ۼ��� ���� ������ ������ �ѱ��� �ʰ� ���� �߰�
        if (level == data.damages.Length)// damages�� ����ִ� ������ ������ ��������
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
