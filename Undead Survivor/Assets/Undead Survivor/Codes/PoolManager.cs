using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    
    // .. �������� ������ ����
    public GameObject[] prefabs; //�������� ����� �迭 ����

    // .. Ǯ ����� �ϴ� ����Ʈ
    List<GameObject>[] pools; //������Ʈ Ǯ���� ������ �迭 ����

    void Awake()
    {
        // ������ ������ ���� ��ŭ�� ����Ʈ�� ����
        pools = new List<GameObject>[prefabs.Length]; //Ǯ�� ��� ����Ʈ �ʱ�ȭ
        // ���� ������ ����Ʈ�� �ʱ�ȭ�� �ʿ���
        for (int i = 0; i < pools.Length; i++)
        {
            //��� ������Ʈ Ǯ ����Ʈ�� �ʱ�ȭ
            pools[i] = new List<GameObject>(); //Ǯ�� �ʱ�ȭ
        }
    }

    //��𼭳� ����� �� �ֵ��� public ����
    public GameObject Get(int index) //��û�� ���� ������Ʈ�� ��ȯ���ֱ����� GameObject������ ����
    {
        GameObject select = null;//���������� �ʱ�ȭ �ʿ�

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����
       foreach (GameObject item in pools[index]) //pools[index]�� �ִ� pool ����Ʈ���� ������Ʈ�� �����鼭 �˻�
        {
            // ... �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf) //������Ʈ�� ��Ȱ��ȭ �Ǿ������� select�� �Ҵ� 
            {
                select = item; //�Ҵ��� ����
                select.SetActive(true); //���� ����� ���̹Ƿ� Ȱ��ȭ
                break;
            }
        }

        // ... ���� ������Ʈ Ǯ�� ��� ������Ʈ�� ������̸�?
        if (!select) //select�� null�̸�? ��Ȱ��ȭ ������Ʈ�� ã�� ���ߴ�.
        {
            // ... �߰����� ������Ʈ�� ���� �� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform); 
            //instantiate �Լ��� �̿��� �������� �����ؼ� ����
            //transform���� �θ� �������� ������ ������Ʈ ������ �ڽ� ������Ʈ ��ġ�� �ƴ϶� �ֿܰ����� �����ȴ�.
            pools[index].Add(select); //���� ������ ������Ʈ�� Ǯ�� �߰����༭ ������ ��Ȱ���� �����ϵ��� �Ѵ�.
        }

        return select;
    }
}
 