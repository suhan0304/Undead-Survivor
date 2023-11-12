using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")] //배경음
    public AudioClip bgmClip;   //클립
    public float bgmVolume;     //볼륨
    AudioSource bgmPlayer;      //플레이어(오디오소스)
    AudioHighPassFilter bgmEffect;  //브금효과

    [Header("#SFX")] //효과음
    public AudioClip[] sfxClips;     //클립
    public float sfxVolume;         //볼륨
    public int channels;            //채널 개수
    AudioSource[] sfxPlayers;        //플레이어(오디오소스)
    int channelIndex;               //재생 중인 채널 인덱스

    //효과음과 1:1 대응하는 열거형 데이터 선언
    public enum Sfx
    {
        //열거형 데이터는 대응하는 숫자를 정해줄 수 있다. 
        //그 이후 데이터는 알아서 +1씩 되면서 진행된다.
        Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win
    }

    private void Awake()
    {
        instance = this;
        Init(); //초기화 진행
    }

    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        //AddComponent로 함수로 오디오소스를 생성하고 변수에 저장
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;  //자동 1회 재생 off
        bgmPlayer.loop = true;          //자동 반복 on
        bgmPlayer.volume = bgmVolume;   //볼륨 설정
        bgmPlayer.clip = bgmClip;       //AudioClip 설정
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];  //채널 수만큼 오디오소스 초기화
        
        //sfxPlayers 안에 오디오소스 하나씩 추가해주면서 각 오디오소스 마다 설정 초기화
        for(int i=0;i<sfxPlayers.Length;i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;  //자동 반복 off
            sfxPlayers[i].bypassListenerEffects = true; //Effect 영향 안받게 설정
            sfxPlayers[i].volume = sfxVolume;   //볼륨 설정
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }    

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        //플레이어에 sfx 인덱스 넣어주고 재생
        for(int i=0;i<sfxPlayers.Length;i++)
        {
            //채널 개수만큼 순회하도록 채널인덱스 변수 활용
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            //재생 중인 플레이어는 continue 시켜버린다.
            if (sfxPlayers[i].isPlaying)
                continue;

            //사운드가 2개 이상있을 경우 랜덤 재생
            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee) {
                ranIndex = Random.Range(0, 2);
                }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

}
