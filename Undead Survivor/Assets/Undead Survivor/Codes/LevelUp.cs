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
}
