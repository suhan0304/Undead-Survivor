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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    
    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) //�׾����� ����
            return;

        Vector2 dirVec = target.position - rigid.position; // ���� = ��ġ ������ ����ȭ (��ġ ���� = Ÿ�� ��ġ - ���� ��ġ)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //����(����ȭ) * �ӵ� * ������ �ð� ����
        rigid.MovePosition(rigid.position + nextVec); //���� ��ġ�� next���͸� ���Ѵ�.
        //�ٸ� ������ٵ�� �ε����� �Ǹ� ���� �ӵ��� ����µ� �츮�� ��ġ �̵��� ä���ϰ� �����Ƿ� ���� �ӵ��� ��ġ�� ��ȭ�ϸ� �ȵǹǷ� velocity�� 0 �����
        rigid.velocity = Vector2.zero; //���� �ӵ��� �̵��� ������ ���� �ʵ��� �ӵ��� ���� 
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive) //�׾����� ����
            return;
 
        //��ǥ�� x��� �ڽ��� x�� ���� ���Ͽ� ������ X���� �������� Flip �ǵ��� FlipX�� True�� ����
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() //Ȱ��ȭ �� �� �� �� ����
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true; //�������� �ʱ�ȭ
        // Dead ������Ʈ�� �ٽ� �ʱ�ȭ
        coll.enabled = true;    
        rigid.simulated = true;    
        spriter.sortingOrder = 2;   
        anim.SetBool("Dead", false);  
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
        if (!collision.CompareTag("Bullet") || !isLive) // �浹�� collision�� Bullet������ ���� Ȯ��
            return;

        health -= collision.GetComponent<Bullet>().damage; //Bullet ��ũ��Ʈ ������Ʈ���� damage�� �����ͼ� ü�¿��� ��´�.
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // .. ���� ������� -> Hit Action 
            anim.SetTrigger("Hit");
        }
        else
        {
            // .. ü���� 0���� ���� -> Die 
            // �Ʒ����� ������ �������� ��Ȱ�뿡�� �� �ٽ� ����� ���� ���� init�� �ʱ�ȭ �ڵ带 �߰� �ۼ�
            coll.enabled = false;       //�浹 ������ ���� collider 2D ��Ȱ��ȭ
            rigid.simulated = false;    //������ Ȱ�� ������ ���� rigidbody 2D ��Ȱ��ȭ
            spriter.sortingOrder = 1;   //Ȱ������ ���͵��� ������ �ʵ��� sortingLayer�� �����
            anim.SetBool("Dead", true);  //Dead �ִϸ��̼��� ���� 
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();

        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // ���� �ϳ��� ���� �������� ������
        Vector3 playerPos = GameManager.Instance.player.transform.position; //Player�� Postion
        Vector3 dirVec = transform.position - playerPos;                    //Player���� Enemy���� ����(=�˹� ����)
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); //�������� ���̹Ƿ� ForceMode2D.Impulse ����
    }

    void Dead()
    {
        //��Ȱ��ȭ�� ���ش�.
        gameObject.SetActive(false);
    }
}
