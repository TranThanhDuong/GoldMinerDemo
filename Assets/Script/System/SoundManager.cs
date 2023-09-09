using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{
    MUSIC_PLAY,
    MUSIC_MENU,
    MUSIC_WIN,
    MUSIC_LOSE,
    BOOM,
    BTN,
    STONE_HIT,
    OTHER_HIT,
    BOTTLE_BREAK,
    MOVE_START,
    SHOOT,
    DRAG,
    MOVE_1,
    MOVE_END,
    GET_POINT,
}
[Serializable]
public class SoundSetting
{
    public SoundType type;
    public AudioClip audioClip;
}

public class SoundManager : Singleton<SoundManager>
{
    public float maxMoveSound = 1.2f;
    public float maxDragAndShootSound = 2.2f;

    public List<SoundSetting> sounds = new List<SoundSetting>();

    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource fxAudioSource;

    Dictionary<SoundType, float> soundTimerDic = new Dictionary<SoundType, float>();

    public void Setup()
    {
        SoundChange(DataAPIControler.Instance.GetSound());
        MusicChange(DataAPIControler.Instance.GetMusic());
        DataTrigger.RegisterValueChange("Sound",(data) => SoundChange((int)data));
        DataTrigger.RegisterValueChange("Music", (data) => MusicChange((int)data));
    }

    void MusicChange(int isOn)
    {
        if (isOn == 1)
        {
            musicAudioSource.enabled = true;
        }
        else
        {
            musicAudioSource.enabled = false;
        }
    }

    void SoundChange(int isOn)
    {
        if (isOn == 1)
        {
            fxAudioSource.enabled = true;
        }
        else
        {
            fxAudioSource.enabled = false;
        }
    }

    //Co the viet theo kieu spawn object tung loai giong pool de xu ly am thanh bat tat khi su dung tung loai sound
    public void PlayMusic(SoundType type)
    {
        if (CanPlaySound(type) && GetAudioClip(type) != null)
        {
            musicAudioSource.clip = GetAudioClip(type);
            musicAudioSource.Play();
            musicAudioSource.loop = true;
        }
    }

    public void PlaySound(SoundType type, Vector3 pos)
    {
        if(CanPlaySound(type) && GetAudioClip(type) != null)
        {
            fxAudioSource.gameObject.transform.position = pos;
            fxAudioSource.clip = GetAudioClip(type);
            fxAudioSource.Play();
        }
    }

    public void PlaySound(SoundType type)
    {
        if (CanPlaySound(type))
        {
            fxAudioSource.clip = GetAudioClip(type);
            fxAudioSource.Play();
        }
    }

    public void StopSound()
    {
        fxAudioSource.Stop();
    }

    public bool CanPlaySound(SoundType type)
    {
        if (type > SoundType.MOVE_START && type < SoundType.MOVE_END)
        {
            if(soundTimerDic.ContainsKey(type))
            {
                float lastPlay = soundTimerDic[type];
                float checkSound = 0;

                switch(type)
                {
                    case SoundType.DRAG:
                    case SoundType.SHOOT:
                        checkSound = maxDragAndShootSound;
                    break;
                    default:
                        checkSound = maxMoveSound;
                    break;
                }    
                if (checkSound + lastPlay < Time.time)
                {
                    soundTimerDic[type] = Time.time;
                    return true;
                }
                else
                    return false;
                
            }
            else
            {
                soundTimerDic.Add(type, 0);
                return true;
            }    
        }
        return true;
    }

    public AudioClip GetAudioClip(SoundType type)
    {
        foreach(SoundSetting sound in sounds)
        {
            if (sound.type == type)
                return sound.audioClip;
        }
        return null;
    }
}
