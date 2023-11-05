using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public int id;          //���� id
    public int prefabId;    //������ id
    public float damage;    //������
    public int count;       //����
    public float speed;     //�ӵ�

    private void Start()
    {
        Init();
    }

    void Update()
    {
        //���� id�� �°� ���� ����
        switch(id)
        {
            case 0:
                //z�� �������� back �������� ȸ�� (Speed�� ������ back �������� ����)
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
                break;
            default:
                break;
        }

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(damage,count);
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage += 3;   //������ 3 ����
        this.count += 1;    //ī��Ʈ 1����

        if (id == 0) //id�� 0�̸� ���ġ
            Batch();
    }


    public void Init()
    {

        //���� id�� �°� ���� �Ӽ��� ����
        switch(id)
        {
            case 0:
                speed = -150;   //���̳ʽ� = �ð����
                Batch();        //���� ��ġ          
                break;
            default:
                break;
        }
    }

    void Batch()// ������ ���⸦ ��ġ�ϴ� �Լ�
    {
        for(int i=0; i<count;i++)
        {
            //prefabId�� �ش��ϴ� prefab�� �������鼭 ���ÿ� transform�� ���������� ����
            Transform bullet;
            // ���� ������Ʈ�� ���� Ȱ���ϰ� ���ڶ� ���� Ǯ������ ��������
            if(i < transform.childCount) // �ڽ��� ������ ������ ���� ������ �ʰ�
            {
                bullet = transform.GetChild(i);  //������ �ڽĵ��� ������ ����.
            }
            else
            {
                bullet= GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;  //���� �������� �͵鸸 parent�� �������ָ� �ȴ�. 
                                            //- ���� �ڽ� ������Ʈ�� ������̴� ���� �̹� ������ �Ǿ�����
            }

            bullet.localPosition = Vector3.zero; //bullet�� localPostion�� 0���� �ʱ�ȭ = �÷��̾��� ��ġ
            bullet.localRotation = Quaternion.identity; //Rotation���� Quaternion�� ��, �ʱⰪ�� identity

            Vector3 rotVec = Vector3.forward * 360 * i / count; //i��° ������ ȸ�� ������ ���
            bullet.Rotate(rotVec);                              //rotVec��ŭ ȸ��
            //Local �������� ������ �������� 1.5��ŭ �̵� 
            //�̵� ������ Space.self�� �ƴ϶� World�� ������? �̹� ȸ�� �� ���� �������� 1.5��ŭ �̵���Ű�� ������ �����Ƿ� �̵� ������ ���带 �������� ����
            bullet.Translate(bullet.up * 1.5f, Space.World);    
            //Bullet�� Bullet ��ũ��Ʈ�� init �Լ��� ������ ���� �ʱ�ȭ
            bullet.GetComponent<Bullet>().init(damage, -1); // -1 is Infinity Per. (���������� ���� ����)
        }
    }
}
