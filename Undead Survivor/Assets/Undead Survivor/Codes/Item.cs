using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;   //아이템 데이터
    public int level;       //레벨 
    public Weapon weapon;   //무기 
    public Gear gear;       //장비 

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        //자식 오브젝트 icon
        icon = GetComponentsInChildren<Image>()[1]; //첫번째[0]는 자기자신
        icon.sprite = data.itemIcon;                //itemData의 아이콘으로 초기화

        Text[] texts = GetComponentsInChildren<Text>(); //자식의 Text 컴포넌트 가져오기
        textLevel = texts[0];   //item 오브젝트에는 Text가 없어서 자식에 있는 Text만 오기 때문에 첫번째[0]로 초기화
        textName = texts[1];    //GetComponents의 순서는 계층 구조의 순서를 따라간다.
        textDesc = texts[2];
        textName.text = data.itemName; //아이템 이름 앞으로도 버튼에 고정이므로 바로 초기화
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
        switch (data.itemType)
        {
            //%는 항상 100을 곱해서 넘겨주기
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                //무기는 입력된 itemDesc의 텍스트의 매개변수에 damages와 counts를 넣어서 문자열 생성
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                //장비는 입력된 itemDesc의 텍스트의 매개변수에 damages와 counts를 넣어서 문자열 생성
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                //음료는 설명만 출력
                textDesc.text = string.Format(data.itemDesc, data.damages[level]);
                break;

        }
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee: // 근접, 원거리 무기는 같은 로직을 사용 
            case ItemData.ItemType.Range: // case를 붙여준다.
                if (level == 0) //레벨이 0일 때 버튼을 누르면 웨폰 오브젝트를 생성
                {
                    GameObject newWeapon = new GameObject();

                    //새로운 오브젝트에 Weapon 컴포넌트 추가
                    //AddComponent 함수 반환 값을 미리 선언한 변수에 저장.
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);

                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level]; //damages를 백분율이기 때문에 곱해서 더해줌
                    nextCount += data.counts[level]; //count는 단순히 counts의 레벨을 인덱스로해서 가져온 값을 더해줌

                    weapon.LevelUp(nextDamage, nextCount); //Weapon의 LevelUp 함수를 이용해 레벨업
                }

                level++;
                break;
            case ItemData.ItemType.Glove: // 무기가 아닌 장비들은 같은 로직을 사용
            case ItemData.ItemType.Shoe:
                if (level == 0) //레벨이 0일 때 버튼을 누르면 장비 오브젝트를 생성
                {
                    GameObject newGear = new GameObject();

                    //새로운 오브젝트에 Weapon 컴포넌트 추가
                    //AddComponent 함수 반환 값을 미리 선언한 변수에 저장.
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];

                    gear.LevelUp(nextRate);
                }

                level++;
                break;
            case ItemData.ItemType.Heal: // 일회성 아이템의 안에서는 LevelUp 함수 실행을 X
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                break;
        }
        // 스크립트블 오브젝트에 작성한 레벨 데이터 개수를 넘기지 않게 로직 추가
        if (level == data.damages.Length)// damages에 들어있는 데이터 개수와 같아지면
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
