using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;   //데미지
    public int per;        //관통

    public void init(float damage, int per)
    {
        this.damage = damage;   //Bullet의 데미지를 매개변수 데미지로 초기화
        this.per = per;         //Bullet의 관통을 매개변수 관통으로 초기화
    }
}
