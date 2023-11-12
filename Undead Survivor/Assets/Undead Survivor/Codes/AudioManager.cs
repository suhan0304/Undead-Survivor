using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")] //�����
    public AudioClip bgmClip;   //Ŭ��
    public float bgmVolume;     //����
    AudioSource bgmPlayer;      //�÷��̾�(������ҽ�)
    AudioHighPassFilter bgmEffect;  //���ȿ��

    [Header("#SFX")] //ȿ����
    public AudioClip[] sfxClips;     //Ŭ��
    public float sfxVolume;         //����
    public int channels;            //ä�� ����
    AudioSource[] sfxPlayers;        //�÷��̾�(������ҽ�)
    int channelIndex;               //��� ���� ä�� �ε���

    //ȿ������ 1:1 �����ϴ� ������ ������ ����
    public enum Sfx
    {
        //������ �����ʹ� �����ϴ� ���ڸ� ������ �� �ִ�. 
        //�� ���� �����ʹ� �˾Ƽ� +1�� �Ǹ鼭 ����ȴ�.
        Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win
    }

    private void Awake()
    {
        instance = this;
        Init(); //�ʱ�ȭ ����
    }

    void Init()
    {
        //����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        //AddComponent�� �Լ��� ������ҽ��� �����ϰ� ������ ����
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;  //�ڵ� 1ȸ ��� off
        bgmPlayer.loop = true;          //�ڵ� �ݺ� on
        bgmPlayer.volume = bgmVolume;   //���� ����
        bgmPlayer.clip = bgmClip;       //AudioClip ����
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        //ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];  //ä�� ����ŭ ������ҽ� �ʱ�ȭ
        
        //sfxPlayers �ȿ� ������ҽ� �ϳ��� �߰����ָ鼭 �� ������ҽ� ���� ���� �ʱ�ȭ
        for(int i=0;i<sfxPlayers.Length;i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;  //�ڵ� �ݺ� off
            sfxPlayers[i].bypassListenerEffects = true; //Effect ���� �ȹް� ����
            sfxPlayers[i].volume = sfxVolume;   //���� ����
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
        //�÷��̾ sfx �ε��� �־��ְ� ���
        for(int i=0;i<sfxPlayers.Length;i++)
        {
            //ä�� ������ŭ ��ȸ�ϵ��� ä���ε��� ���� Ȱ��
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            //��� ���� �÷��̾�� continue ���ѹ�����.
            if (sfxPlayers[i].isPlaying)
                continue;

            //���尡 2�� �̻����� ��� ���� ���
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
