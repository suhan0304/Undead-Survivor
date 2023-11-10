using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        
    }

    //������ â�� �����ִ� �Լ�
    public void Show()
    {
        rect.localScale = Vector3.one;
    }

    //������ â�� ����� �Լ�
    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }
}
