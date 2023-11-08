using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate() //�÷��̾ ���� ������ ������ �̵� ��
    {
        //������ǥ�� ��ũ�� ��ǥ�� ���� �ٸ��� ������ �ٲ��� �Ŀ� ����
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position);
    }
}
