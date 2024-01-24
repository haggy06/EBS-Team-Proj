using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerUI : PopupBase
{
    [Space(5), SerializeField]
    private RectTransform stressNeedle;

    [SerializeField]
    private Image staminaGauge;

    private Vector3 rotateVec = Vector3.zero;
    public new void InitInfo()
    {
        base.InitInfo();

        rotateVec.z = GameManager.Inst.CurStress * 180f;

        stressNeedle.localEulerAngles = rotateVec;
    }

    [ContextMenu("Stress Renewal - Lerp")]
    public void StressRenewal_Lerp()
    {
        LeanTween.value(stressNeedle.gameObject, stressNeedle.localEulerAngles.z, (GameManager.Inst.CurStress + 1) * 90f, 1.5f).setEase(LeanTweenType.easeOutCirc).setOnUpdate((float value) => { NeedleMove(value); });
    }
    [ContextMenu("Stress Renewal - Instant")]
    public void StressRenewal_Instant()
    {
        NeedleMove((GameManager.Inst.CurStress + 1) * 90f);
    }
    private void NeedleMove(float value)
    {
        rotateVec.z = value;

        stressNeedle.localEulerAngles = rotateVec;
    }

    public void StaminaRenewal(float newPercent)
    {
        staminaGauge.fillAmount = newPercent;
    }
}
