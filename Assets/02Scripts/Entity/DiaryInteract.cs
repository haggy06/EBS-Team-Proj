using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryInteract : InteractBase
{
    public override void Interact()
    {
        base.Interact();

        InvincibleCanvasManager.Inst.PopupOpen(InvincibleCanvasManager.Inst.DiaryPopup);
    }
}
