using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHome : SceneMoveInteract
{
    public override void Interact()
    {
        base.Interact();

        GameObject.FindWithTag("Monster").GetComponent<Monster>().Forgive();
    }
}
