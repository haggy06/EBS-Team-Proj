using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInteract : TalkInteract
{
    protected override void OnTriggerEnter(Collider other)
    {
        Interact();

        StartCoroutine("TalkOver");
    }

    private IEnumerator TalkOver()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        while (InvincibleCanvasManager.Inst.Talk_UI.IsON)
        {
            yield return null;
        }

        GameManager.Inst.SelectScene(SCENE.Road);
    }
}
