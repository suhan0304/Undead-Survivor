using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        //static은 인스턴스에 나오지 않으므로 초기화 해줘야함
        Instance = this;
    }


}
