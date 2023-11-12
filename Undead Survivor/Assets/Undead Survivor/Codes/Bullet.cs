using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;   //데미지
    public int per;        //관통

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;   //Bullet의 데미지를 매개변수 데미지로 초기화
        this.per = per;         //Bullet의 관통을 매개변수 관통으로 초기화

        if(per >= 0)
        {
            rigid.velocity = dir * 15f; //속도는 15로 사용 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터와 만난게 아니거나, 관통이 무한일 경우 return
        if (!collision.CompareTag("Enemy") || per == -100)
            return;

        per--;

        if(per < 0) //per 수치만큼 몬스터를 만나서 0보다 작아지면 삭제
        {
            rigid.velocity = Vector2.zero;//비활성화 이전에 재사용을 위해 미리 물리 속도 초기화
            gameObject.SetActive(false); //비활성화
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false);
    }
}
