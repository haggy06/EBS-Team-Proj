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

    private Button myBtn;
    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        myBtn = GetComponent<Button>();
    }

    public void EventChange()
    {
        myBtn.onClick.RemoveAllListeners();

        // myBtn.onClick.AddListener();
    }
}
