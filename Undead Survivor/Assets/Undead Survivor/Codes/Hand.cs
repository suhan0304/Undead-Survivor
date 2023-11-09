using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft; //�޼����� ���������� ����
    public SpriteRenderer spriter;

    SpriteRenderer player; //�÷��̾��� ��������Ʈ �������� ������ �޾ƿ� FlipX ���θ� Ȯ��

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0f);          // ������ ��ġ
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0f);  // ���� ������ ������ ��ġ

    Quaternion leftRot = Quaternion.Euler(0, 0, -35);       // �޼��� ȸ���� �����ϱ� ���� Quaternion �� ���
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);      // ���� ������ ȸ��


    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; 
        //�θ��� spriteRenderer�� [1]���� [0]�� �ڱ� �ڽ��� SpriteRenderer
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft) // ���� ����
        {
            //�׻� �÷��̾� �������� ��ġ, ȸ���ϱ� ������ local�� ����ؾ��Ѵ�.
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse; //�޼� ��������Ʈ(���� ����)�� Y�� ����
            spriter.sortingOrder = isReverse ? 4 : 6; //���� ���ο� ���� order Layer ����
        }
        else // ���Ÿ� ����
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;  //������ ��������Ʈ(���Ÿ� ����)�� X�� ����
            spriter.sortingOrder = isReverse ? 6 : 4; //���� ���ο� ���� order Layer ����
        }
    }
}
