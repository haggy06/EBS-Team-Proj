using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteract : SceneMoveInteract
{
    public override void Interact()
    {
        base.Interact();

        GameManager.Inst.Play_Data.curDay++;
    }
}
