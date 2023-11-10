using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); //true�� ������ ��Ȱ��ȭ �� ������Ʈ�� ������Ʈ�� ������ �� �ִ�.
    }
    
    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) //����������� ����
            return;

        // �����Ӹ��� �̺�Ʈ�� ����Ǳ� ������ DeltaTime�� �����־�� �Ѵ�.
        GameManager.Instance.health -= Time.deltaTime * 10;

        if(GameManager.Instance.health < 0)
        {
            for(int i=2;i<transform.childCount;i++) //�ڽ� ������Ʈ���� ��Ȱ��ȭ
            {
                //Spawner���� Hand ��� ��Ȱ��ȭ
                transform.GetChild(i).gameObject.SetActive(false); 
                //�ڽ��� transform�� ��ȯ�ǹǷ� �ٽ� gameobject�� �ö󰡼� ��Ȱ��ȭ
            }

            //���� �ִϸ��̼� ����
            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
}
