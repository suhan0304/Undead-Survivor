using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    
    // .. 프리팹을 보관할 변수
    public GameObject[] prefabs; //프리팹이 저장될 배열 변수

    // .. 풀 담당을 하는 리스트
    List<GameObject>[] pools; //오브젝트 풀들을 저장할 배열 변수

    void Awake()
    {
        // 각각의 프리팹 길이 만큼의 리스트를 생성
        pools = new List<GameObject>[prefabs.Length]; //풀을 담는 리스트 초기화
        // 새로 생성한 리스트도 초기화가 필요함
        for (int i = 0; i < pools.Length; i++)
        {
            //모든 오브젝트 풀 리스트를 초기화
            pools[i] = new List<GameObject>(); //풀을 초기화
        }
    }

    //어디서나 사용할 수 있도록 public 선언
    public GameObject Get(int index) //요청한 게임 오브젝트를 반환해주기위해 GameObject형으로 선언
    {
        GameObject select = null;//지역변수는 초기화 필요

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
       foreach (GameObject item in pools[index]) //pools[index]에 있는 pool 리스트에서 오브젝트를 꺼내면서 검사
        {
            // ... 발견하면 select 변수에 할당
            if (!item.activeSelf) //오브젝트가 비활성화 되어있으면 select에 할당 
            {
                select = item; //할당을 진행
                select.SetActive(true); //이제 사용할 것이므로 활성화
                break;
            }
        }

        // ... 만약 오브젝트 풀의 모든 오브젝트가 사용중이면?
        if (!select) //select가 null이면? 비활성화 오브젝트를 찾지 못했다.
        {
            // ... 추가적인 오브젝트를 생성 후 select 변수에 할당
            select = Instantiate(prefabs[index], transform); 
            //instantiate 함수를 이용해 프리팹을 복사해서 생성
            //transform으로 부모를 맞춰주지 않으면 오브젝트 생성이 자식 오브젝트 위치가 아니라 최외곽에서 생성된다.
            pools[index].Add(select); //새로 생성한 오브젝트도 풀에 추가해줘서 앞으로 재활용이 가능하도록 한다.
        }

        return select;
    }
}
 