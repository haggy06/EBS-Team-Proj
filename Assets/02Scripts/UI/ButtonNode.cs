using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ButtonNode : MonoBehaviour
{
    #region _Button Nodes_
    [SerializeField]
    protected ButtonNode upBtn;
    public ButtonNode UpBtn
    {
        get => upBtn;
        set => upBtn = value;
    }

    [SerializeField]
    protected ButtonNode downBtn;
    public ButtonNode DownBtn
    {
        get => downBtn;
        set => downBtn = value;
    }

    [SerializeField]
    protected ButtonNode rightBtn;
    public ButtonNode RightBtn
    {
        get => rightBtn;
        set => rightBtn = value;
    }

    [SerializeField]
    protected ButtonNode leftBtn;
    public ButtonNode LeftBtn
    {
        get => leftBtn;
        set => leftBtn = value;
    }
    #endregion
    protected Button btn;

    protected Animator anim;

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

    public void Highlight_This() // ��ư�� Highlight �ִϸ��̼ǿ��� �̺�Ʈ�� �����ų ����
    {
        if (InvincibleCanvasManager.Inst.CurBtn != this)
        {
            InvincibleCanvasManager.Inst.SelectBtnChange(this);
        }
    }

    public virtual void BtnClick()
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
            Debug.Log(gameObject.name + " ��ư ���̶���Ʈ");
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
