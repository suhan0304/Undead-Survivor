using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;   //������
    public int per;        //����

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;   //Bullet�� �������� �Ű����� �������� �ʱ�ȭ
        this.per = per;         //Bullet�� ������ �Ű����� �������� �ʱ�ȭ

        if(per > -1)
        {
            rigid.velocity = dir * 15f; //�ӵ��� 15�� ��� 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���Ϳ� ������ �ƴϰų�, ������ ������ ��� return
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if(per == -1) //per ��ġ��ŭ ���͸� ������ -1���� ����
        {
            rigid.velocity = Vector2.zero;//��Ȱ��ȭ ������ ������ ���� �̸� ���� �ӵ� �ʱ�ȭ
            gameObject.SetActive(false); //��Ȱ��ȭ
        }
    }

    void Update() //Dead�� ������ ������ ����
    {
        Dead();
    }

    void Dead() //player���� �Ÿ��� 20���� �־����� ��Ȱ��ȭ �ǵ��� ����
    {
        Transform target = GameManager.Instance.player.transform;
        Vector3 targetPos = target.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 20f && per != -1)
        {
            rigid.velocity = Vector2.zero;//��Ȱ��ȭ ������ ������ ���� �̸� ���� �ӵ� �ʱ�ȭ
            this.gameObject.SetActive(false); //��Ȱ��ȭ
        }
    }
}
