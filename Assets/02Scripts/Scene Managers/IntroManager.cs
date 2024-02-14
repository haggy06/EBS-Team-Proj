using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private void Awake()
    {
        InvincibleCanvasManager.Inst.Fade_Popup.CanvasShow();
        InvincibleCanvasManager.Inst.Fade_Popup.CanvasFadeOut();

        Invoke("MoveToTitle", 2);
    }

    private void MoveToTitle()
    {
        GameManager.Inst.CurrentScene = SCENE.Title;

        InvincibleCanvasManager.Inst.Fade_Popup.StartFade(FadeMode.LoadScene);
    }
}
