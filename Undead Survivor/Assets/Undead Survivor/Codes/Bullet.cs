using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;   //������
    public int per;        //����

    public void init(float damage, int per)
    {
        this.damage = damage;   //Bullet�� �������� �Ű����� �������� �ʱ�ȭ
        this.per = per;         //Bullet�� ������ �Ű����� �������� �ʱ�ȭ
    }
}
