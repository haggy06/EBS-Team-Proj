using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TalkUI : PopupBase
{
    [Space(5)]

    [SerializeField]
    private Transform playerBody;

    private Emotion lastEmotion;
    protected override void OnActive()
    {
        playerBody.GetChild((int)lastEmotion).GetComponent<Image>().enabled = false;

        lastEmotion = GameManager.Inst.CurEmotion;

        playerBody.GetChild((int)GameManager.Inst.CurEmotion).GetComponent<Image>().enabled = true;
    }


}
