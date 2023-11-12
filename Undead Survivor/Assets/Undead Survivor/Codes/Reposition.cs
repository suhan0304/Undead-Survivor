using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    //Trigger�� check�� collider���� �������� ȣ��
    void OnTriggerExit2D(Collider2D collision)
    {

        if(!collision.CompareTag("Area")) //���� Collision�� Tag�� Area�� �ƴϸ� �ٷ� return
            return;


        //Player�� ��ġ�� ������
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        //�� ��ġ�� ������
        Vector3 myPos = transform.position;

        //collsion�� � �±��̳Ŀ� ���� ������ �ٸ�
        //���Ŀ� ���͵� ���ġ�� ���ֱ� ���� ����
        switch (transform.tag)
        {
            case "Ground": //collsion�� �±װ� ground�� ���

                float diffX = playerPos.x - myPos.x; //x�� ��ǥ ����
                float diffY = playerPos.y - myPos.y; //y�� ��ǥ ����
                float dirX = diffX < 0 ? -1 : 1; //player�� inputVec�� x�� �����̴�? ������� ����(-1), �ƴҰ�� ������(1)
                float dirY = diffY < 0 ? -1 : 1; //player�� inputVec�� y�� �����̴�? ������� �Ʒ���(-1), �ƴҰ�� ����(1)
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY) //�� ������Ʈ�� �Ÿ� ���̿��� X���� Y�ຸ�� ũ�� ���� �����̵�
                {
                    //Translate ������ �� ��ŭ ���� ��ġ���� �̵�
                    transform.Translate(Vector3.right * dirX * 40);  //������ ���� ����(1, 0, 0) * ����(���� -1 , ������ 1) * ũ��(40) 
                                                                        //ũ�Ⱑ 40�� ������ Ÿ�ϸ��� 4���� ����ؼ� 2*2�� �����߱� ����
                }
                else if (diffX < diffY)  //�� ������Ʈ�� �Ÿ� ���̿��� Y���� X�ຸ�� ũ�� ���� �����̵�
                {
                    //Translate ������ �� ��ŭ ���� ��ġ���� �̵�
                    transform.Translate(Vector3.up * dirY * 40); 
                }
                else
                {
                    transform.Translate(dirX * 40, dirY * 40, 0);
                }
                break;

            case "Enemy": //collsion�� �±װ� Enemy�� ���
                if (coll.enabled) // �ݶ��̴��� Ȱ��ȭ �Ǿ��ִ��� ���� ���� �ۼ� - ���Ͱ� �׾����� ��Ȱ��ȭ �Ǿ����� => ���ġ �ʿ䰡 ����
                {
                    Vector3 dist = playerPos - myPos; // �� -> �÷��̾� ���Ͱ� ������� (�� ���͸�ŭ ���� �ű�� �÷��̾���ġ, 2�� �ű�� �÷��̾��� ���ʿ� ��ġ)
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0); // ���� ���͸� ���Ͽ� �����ִ� ���� ���ġ ����
                    transform.Translate(ran + dist * 2); 
                }
                break;
        }


    } 
}
