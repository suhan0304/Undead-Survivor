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

    void FixedUpdate() //플레이어가 물리 프레임 단위로 이동 중
    {
        //월드좌표와 스크린 좌표는 서로 다르기 때문에 바꿔준 후에 설정
        rect.position = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position);
    }
}
