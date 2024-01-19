using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ButtonNode : MonoBehaviour
{
    #region _Button Nodes_
    [SerializeField]
    private ButtonNode upBtn;
    public ButtonNode UpBtn => upBtn;

    [SerializeField]
    private ButtonNode downBtn;
    public ButtonNode DownBtn => downBtn;

    [SerializeField]
    private ButtonNode rightBtn;
    public ButtonNode RightBtn => rightBtn;

    [SerializeField]
    private ButtonNode leftBtn;
    public ButtonNode LeftBtn => leftBtn;
    #endregion
    private Button btn;

    private Animator anim;

    private void Awake()
    {
        btn = GetComponent<Button>();
        TryGetComponent<Animator>(out anim);

        StartCoroutine("Test");
    }
    private IEnumerator Test()
    {
        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.5f);

            //anim.SetTrigger(ButtonHashes.Highlighted);
        }
    }

    public void Highlight_This() // 버튼의 Highlight 애니메이션에서 이벤트로 실행시킬 예정
    {
        if (InvincibleCanvasManager.Inst.CurBtn != this)
        {
            InvincibleCanvasManager.Inst.SelectBtnChange(this);
        }
    }

    public void BtnClick()
    {
        if (anim != null)
        {
            anim.SetTrigger(ButtonHashes.Pressed);
            anim.SetTrigger(ButtonHashes.Selected);
        }

        btn.onClick.Invoke();
    }

    public void Selected()
    {
        if (anim != null)
        {
            Debug.Log(gameObject.name + " 버튼 하이라이트");
            anim.SetTrigger(ButtonHashes.Highlighted);
        }
    }
    public void DisSelected()
    {
        if (anim != null)
        {
            anim.SetTrigger(ButtonHashes.Normal);
        }
    }
}
