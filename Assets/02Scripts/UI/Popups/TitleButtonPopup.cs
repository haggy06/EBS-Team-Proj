using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TitleButtonPopup : PopupBase
{
    private void Awake()
    {
        InitInfo();

        Button btn;

        btn = transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(() => GameManager.Inst.SelectScene(SCENE.SampleScene0));
    }
}
