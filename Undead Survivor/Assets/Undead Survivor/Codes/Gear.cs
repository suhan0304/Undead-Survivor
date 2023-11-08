using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.Instance.player.transform; 
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0]; //Gear�� �ֿ� rate�� damages�� ���� ��

        ApplyGear(); //�� ó�� ������ �� ���� ���� �Լ��� ȣ��
    }
    
    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();    //������ �� �� ���� ���� �Լ��� ȣ��
    }

    void ApplyGear() //Ÿ�Կ� ���� �����ϰ� ������ ���� �����ִ� �Լ� �߰�
    {
        switch(type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp() //�尩�� ����� ������� �ø��� �Լ�
    {
        // �θ�� �ö󰡼� �ڽĵ��� Weapon ������Ʈ���� �� �����´�.
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        //���� �ϳ��� ��ȸ�ϸ鼭 Ÿ�Կ� ���� speed �� ����
        foreach(Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0: //�ٰŸ� ������ ���
                    weapon.speed = 150 + (150 * rate); // ȸ�� �ӵ��� ����
                    break;
                default: //���Ÿ� ������ ���
                    weapon.speed = 0.5f * (1f - rate);  // �߻� �ֱ⸦ ����
                    break;
            }

        }
    }

    void SpeedUp()
    {
        float speed = 3; //�⺻ �̵��ӵ�
        GameManager.Instance.player.speed = speed + speed * rate; //�÷��̾��� �̵��ӵ� ����
    }
}
