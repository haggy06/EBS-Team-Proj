using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ChoiceButton : ButtonNode
{
    [SerializeField]
    private TextMeshProUGUI text;
    public TextMeshProUGUI Text => text;

    private new void Awake()
    {
        base.Awake();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        btn.onClick.AddListener(() => InvincibleCanvasManager.Inst.Talk_UI.SetTextClass(textID));
        btn.onClick.AddListener(() => GameManager.Inst.StressChange(stressEvent));
        btn.onClick.AddListener(() => InvincibleCanvasManager.Inst.SelectBtnChange(null));
        btn.onClick.AddListener(InvincibleCanvasManager.Inst.Choice_Popup.ChoiceEnd);
    }
    [SerializeField]
    private int textID = 0;

    [SerializeField]
    private int stressEvent = 0;
    public void EventChange(int id, int stress)
    {
        textID = id;
        stressEvent = stress;
    }
}
