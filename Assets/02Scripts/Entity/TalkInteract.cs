using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : InteractBase
{
    [SerializeField] string characterName;

     public override void Interact()
    {
        base.Interact();

        InvincibleCanvasManager.Inst.Talk_UI.CanvasFadeIn();
    }
}
