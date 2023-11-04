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
        float diffX = Mathf.Abs(playerPos.x - myPos.x); //x�� ��ǥ ����
        float diffY = Mathf.Abs(playerPos.y - myPos.y); //y�� ��ǥ ����

        //�÷��̾��� ������ �ľ�
        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1; //player�� inputVec�� x�� �����̴�? ������� ����(-1), �ƴҰ�� ������(1)
        float dirY = playerDir.y < 0 ? -1 : 1; //player�� inputVec�� y�� �����̴�? ������� �Ʒ���(-1), �ƴҰ�� ����(1)

        //collsion�� � �±��̳Ŀ� ���� ������ �ٸ�
        //���Ŀ� ���͵� ���ġ�� ���ֱ� ���� ����
        switch (transform.tag)
        {
            case "Ground": //collsion�� �±װ� ground�� ���
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
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f)); 
                    //��� �� ũ�⸸ŭ�� �̵� -> �÷��̾��� �þ� �ۿ��� ���ġ �ǵ��� 
                }
                break;
        }


    } 
}
