using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveInteract : InteractBase
{
    [Space(5), SerializeField]
    private SCENE targetScene;

    public override void Interact()
    {
        base.Interact();

        GameManager.Inst.SelectScene(targetScene);
    }
}
