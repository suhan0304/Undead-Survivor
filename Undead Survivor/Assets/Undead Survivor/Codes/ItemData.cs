using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    //���� Ÿ�� : �ٰŸ�, ���Ÿ�, �尩, �Ź�, ��
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    // �������� ���� �Ӽ����� ������ �ۼ�
    [Header("# Main Info ")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;//�ʱ� ������
    public int baseCount;   //�ʱ� ī��Ʈ
    public float[] damages; //���� �� ������
    public int[] counts;    //���� �� ī��Ʈ

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
