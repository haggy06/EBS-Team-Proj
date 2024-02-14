using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgm;

    private void Start()
    {
        if (bgm != null)
        {
            AudioManager.Inst.ChangeBGM(bgm);

            AudioManager.Inst.PlayBGM_Lerp(true);
        }
        else
        {
            AudioManager.Inst.PlayBGM_Lerp(false);
        }
    }
}
