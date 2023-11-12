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
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    //레벨업 창을 숨기는 함수
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].OnClick();
   
    }

    void Next()
    {
        //1. 모든 아이템 비활성화
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false); //item 컴포넌트의 게임오브젝트로 넘어가 비활성화
        }

        //2. 랜덤 아이템 3개 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[2] != ran[0])
                break;
        }

        for(int i=0;i<ran.Length;i++)
        {
            Item ranItem = items[ran[i]];

            //3. 만렙 아이템의 경우는 소비 아이템으로 대체
            if (ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
        
    }
}
