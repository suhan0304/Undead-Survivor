using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;         //�ӵ�
    public float health;        //ü��
    public float maxHealth;     //�ִ� ü��
    public RuntimeAnimatorController[] animCon;     //������ �ִϸ����͸� �ٲٱ� ���� ��Ʈ�ѷ�
    public Rigidbody2D target;  //��ǥ Rigidbody

    bool isLive; //��������

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        if (!isLive) //�׾����� ����
            return;

        Vector2 dirVec = target.position - rigid.position; // ���� = ��ġ ������ ����ȭ (��ġ ���� = Ÿ�� ��ġ - ���� ��ġ)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //����(����ȭ) * �ӵ� * ������ �ð� ����
        rigid.MovePosition(rigid.position + nextVec); //���� ��ġ�� next���͸� ���Ѵ�.
        //�ٸ� ������ٵ�� �ε����� �Ǹ� ���� �ӵ��� ����µ� �츮�� ��ġ �̵��� ä���ϰ� �����Ƿ� ���� �ӵ��� ��ġ�� ��ȭ�ϸ� �ȵǹǷ� velocity�� 0 �����
        rigid.velocity = Vector2.zero; //���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ��� ���� 
    }

    private void LateUpdate()
    {
        if (!isLive) //�׾����� ����
            return;
 
        //��ǥ�� x��� �ڽ��� x�� ���� ���Ͽ� ������ X���� �������� Flip �ǵ��� FlipX�� True�� ����
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() //Ȱ��ȭ �� �� �� �� ����
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true; //�������� �ʱ�ȭ
        health = maxHealth;
    }

    public void Init(SpawnData data) //�ʱ� �Ӽ��� �����ϴ� �Լ� �ۼ�
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];  //�ִϸ��̼� ����
        speed = data.speed;         //�ӵ� ����
        maxHealth = data.health;    //ü�� ����
        health = data.health;                                           
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) // �浹�� collision�� Bullet������ ���� Ȯ��
            return;

        health -= collision.GetComponent<Bullet>().damage; //Bullet ��ũ��Ʈ ������Ʈ���� damage�� �����ͼ� ü�¿��� ��´�.

        if (health > 0)
        {
            // .. ���� ������� -> Hit Action 
        }
        else
        {
            // .. ü���� 0���� ���� -> Die 
            Dead();
        }
    }

    void Dead()
    {
        //��Ȱ��ȭ�� ���ش�.
        gameObject.SetActive(false);
    }
}
