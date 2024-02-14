using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class DiaryInteract : InteractBase
{
    public override void Interact()
    {
        base.Interact();

        InvincibleCanvasManager.Inst.PopupOpen(InvincibleCanvasManager.Inst.Diary_Popup);
    }
}
