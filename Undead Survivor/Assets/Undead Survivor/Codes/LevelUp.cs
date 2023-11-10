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
        items = GetComponentsInChildren<Item>(true); // true면 비활성화 된 item 컴포넌트도 가져온다.
        
    }

    //레벨업 창을 보여주는 함수
    public void Show()
    {
        rect.localScale = Vector3.one;
    }

    //레벨업 창을 숨기는 함수
    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }
}
