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
        Next();
        rect.localScale = Vector3.one;
        GameManager.Instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    //������ â�� ����� �Լ�
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
        //1. ��� ������ ��Ȱ��ȭ
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false); //item ������Ʈ�� ���ӿ�����Ʈ�� �Ѿ ��Ȱ��ȭ
        }

        //2. ���� ������ 3�� Ȱ��ȭ
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

            //3. ���� �������� ���� �Һ� ���������� ��ü
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
