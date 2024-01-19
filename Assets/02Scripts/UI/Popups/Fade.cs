using UnityEngine;

public enum FadeMode
{
    LoadScene,
    GameQuit,

}

public class Fade : PopupBase
{
    private FadeMode fadeMode;

    public void StartFade(FadeMode newFadeMode)
    {
        fadeMode = newFadeMode;

        CanvasFadeIn();
    }

    public override void InitInfo()
    {
        base.InitInfo();
    }
    protected override void FadeInFinished()
    {
        switch (fadeMode)
        {
            case FadeMode.LoadScene:
                GameManager.Inst.GoLoadingScene();
                break;

            case FadeMode.GameQuit:
                GameManager.Inst.GameQuit();
                break;

            default:
                Debug.LogError("Fademode가 지정되어 있지 않습니다.");
                break;
        }
    }
}
