using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : InteractBase
{
    [SerializeField]
    protected int talkID;
    public int TalkID { set => talkID = value; }

     public override void Interact()
    {
        base.Interact();

        if (talkID != 0)
        {
            InvincibleCanvasManager.Inst.Talk_UI.CanvasFadeIn();
            InvincibleCanvasManager.Inst.Talk_UI.SetTextClass(talkID);
        }
        else
        {
            Invoke("ControllON", 0.1f);
        }

        if (TryGetComponent<Collider>(out Collider col))
        {
            col.enabled = false;
        }
    }

    private void ControllON()
    {
        GameManager.Inst.CurPlayer.ControllSwitch(true);
    }
}
