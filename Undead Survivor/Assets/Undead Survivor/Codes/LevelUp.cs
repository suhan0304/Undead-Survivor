using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true); // true�� ��Ȱ��ȭ �� item ������Ʈ�� �����´�.
        
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

    public void Select(int index)
    {
        items[index].OnClick();
    }
}
