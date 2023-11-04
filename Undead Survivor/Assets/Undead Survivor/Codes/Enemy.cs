using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;         //�ӵ�
    public Rigidbody2D target;  //��ǥ Rigidbody

    bool isLive = true; //��������

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }

}
