using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : InteractBase
{
    [SerializeField]
    private int talkID;

     public override void Interact()
    {
        base.Interact();

        InvincibleCanvasManager.Inst.Talk_UI.CanvasFadeIn();
        InvincibleCanvasManager.Inst.Talk_UI.SetTextClass(talkID);
    }
}
