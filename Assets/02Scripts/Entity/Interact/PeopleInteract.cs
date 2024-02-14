using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleInteract : TalkInteract
{
    public override void Interact()
    {
        base.Interact();

        GameObject.FindWithTag("SceneMove").GetComponent<SceneMoveInteract>().TalkCount++;
    }
}
