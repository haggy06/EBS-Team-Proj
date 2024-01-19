using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class PopupBase : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 0.25f;
    public float FadeDuration
    {
        set => fadeDuration = value;
    }

    [SerializeField]
    protected CanvasGroup myCanvasGroup;
    public CanvasGroup MyCanvasGroup => myCanvasGroup;

    [SerializeField, Tooltip("팝업이 떴을 때 자동으로 선택될 버튼. 비워둬도 됨")]
    private ButtonNode firstBtnNode;
    public ButtonNode FirstBtnNode => firstBtnNode;

    /*
    [SerializeField]
    private SelectButtonToKeyboard myButtonGrid;
    */

    public virtual void InitInfo()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
    }

    #region _FadeIn_
    [ContextMenu("Appear")]
    public void CanvasFadeIn()
    {
        //Invoke("OnActive", 0.02f);
        OnActive();

        LeanTween.alphaCanvas(myCanvasGroup, 1f, fadeDuration).setEase(LeanTweenType.linear).setIgnoreTimeScale(true).setOnComplete(FadeInFinished);

        //Invoke("CanvasActive", 0.02f);
        CanvasActive();
    }
    protected virtual void OnActive()
    {

    }
    protected virtual void FadeInFinished()
    {

    }
    #endregion

    #region _FadeOut_
    [ContextMenu("Disappear")]
    public void CanvasFadeOut()
    {
        //Invoke("OnDeactive", 0.02f);
        OnDeactive();

        LeanTween.alphaCanvas(myCanvasGroup, 0f, fadeDuration).setEase(LeanTweenType.linear).setIgnoreTimeScale(true).setOnComplete(FadeOutFinished);

        //Invoke("CanvasDeactive", 0.02f);
        CanvasDeactive();
    }
    protected virtual void OnDeactive()
    {

    }
    protected virtual void FadeOutFinished()
    {

    }
    #endregion

    private void CanvasDeactive()
    {
        Debug.Log(gameObject.name + " 비활성화");

        myCanvasGroup.blocksRaycasts = false;
        myCanvasGroup.interactable = false;
    }

    private void CanvasActive()
    {
        Debug.Log(gameObject.name + " 활성화");

        myCanvasGroup.blocksRaycasts = true;
        myCanvasGroup.interactable = true;
    }

    public void CanvasHide()
    {
        CanvasDeactive();

        myCanvasGroup.alpha = 0;
    }

    public void CanvasShow()
    {
        CanvasActive();

        myCanvasGroup.alpha = 1;
    }


}
