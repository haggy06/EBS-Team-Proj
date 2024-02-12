using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

//효과음 목록
public enum Sfx { ButtonClick, Walk, Run, Exhaustion }

public class AudioManager : MonoSingleton<AudioManager>
{
    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.2f;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips = new AudioClip[20];
    public float sfxVolume = 0.5f;
    public int channels = 8;
    AudioSource[] sfxPlayers;
    int channelIndex;

    [SerializeField]
    private float duration = 1f;

    public void InputAudioClip(Sfx sfx)
    {
        sfxClips[(int)sfx] = Resources.Load<AudioClip>(Path.Combine("MonoSingletons", sfx.ToString()));
    }

    private AudioSource bgm;
    private AudioSource sfx;
    private new void Awake()
    {
        base.Awake();

        InputAudioClip(Sfx.ButtonClick);
        InputAudioClip(Sfx.Walk);
        InputAudioClip(Sfx.Run);
        InputAudioClip(Sfx.Exhaustion);

        bgm = transform.GetChild(0).GetComponent<AudioSource>();
        sfx = transform.GetChild(1).GetComponent<AudioSource>();
    }
    public void PlayBGM_Instant(bool isPlay)
    {
        if (isPlay)
        {
            bgm.Play();
        }
        else
        {
            bgm.Stop();
        }
    }
    public void PlayBGM_Lerp(bool isPlay)
    {
        if (isPlay)
        {
            bgm.Play();
            LeanTween.value(bgm.volume, 1f, duration).setOnUpdate((float value) => bgm.volume = value);
        }
        else
        {
            LeanTween.value(bgm.volume, 0f, duration).setOnUpdate((float value) => bgm.volume = value).setOnComplete(bgm.Stop);
        }
    }
    public void ChangeBGM(AudioClip source)
    {
        bgm.clip = source;
    }

    public void PlaySfx(Sfx clip)
    {
        sfx.clip = sfxClips[(int)clip];
        sfx.Play();
    }
}
