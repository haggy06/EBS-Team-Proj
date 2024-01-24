using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAlert : PopupBase
{
    protected override void OnActive()
    {
        StartCoroutine("TargetFollow");
    }

    protected override void FadeOutFinished()
    {
        StopCoroutine("TargetFollow");
    }

    private IEnumerator TargetFollow()
    {
        Transform targetTrans = InvincibleCanvasManager.Inst.CurActTarget.transform;
        while (true)
        {
            transform.position = Camera.main.WorldToScreenPoint(targetTrans.position + Vector3.up);

            yield return null;
        }
    }
}
