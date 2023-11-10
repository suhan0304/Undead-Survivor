using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    //무기 타입 : 근거리, 원거리, 장갑, 신발, 힐
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    // 아이템의 각종 속성들을 변수로 작성
    [Header("# Main Info ")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;//초기 데미지
    public int baseCount;   //초기 카운트
    public float[] damages; //레벨 업 데미지
    public int[] counts;    //레벨 업 카운트

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
