using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;         //��ĵ ������ ����
    public LayerMask targetLayer;   //��ĵ�� ���̾�
    public RaycastHit2D[] targets;  //��ĵ ��� �迭
    public Transform nearestTarget; //���� ����� ��ǥ
    
    void FixedUpdate()
    {
        // ������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ� 
        //�츮�� ����? �÷��̾ �߽����� ������ ��ü�� Ž��
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        //CircleCastAll     (1.ĳ���� ������ġ, 
        //                   2. ���� ������, 
        //                   3. ĳ���� ����,  => ���� �츮�� Ư�� �������� ĳ��Ʈ�� ��� ���� ������ �ƴϹǷ�Vector2.zero ���
        //                   4. ĳ���� ����,  => ĳ������ ��� ���� ������ �ƴ϶� ���̰� 0 
        //                   5. ��� ���̾�) 
        nearestTarget = GetNearest();
    }

    //���� ����� ������Ʈ�� Transform�� ��ȯ ������ �Լ�
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100; //�÷��̾���� �Ÿ�

        //foreach ������ ĳ���� ��� ������Ʈ�� �ϳ��� �湮
        foreach (RaycastHit2D target in targets)
        {  //CircleCastAll�� ���� �ֵ��� �ϳ��� ����
            Vector3 myPos = transform.position;             //�÷��̾� ��ġ
            Vector3 targetPos = target.transform.position;  //Ÿ���� ��ġ
            float curDiff = Vector3.Distance(myPos, targetPos); //�Ÿ��� ���Ѵ�.

            if (curDiff < diff) //�ּ� �Ÿ��� ����
            {
                diff = curDiff;
                result = target.transform; //����� ���� ����� Ÿ���� transform���� ����
            }

        }

        return result;
    }
}
