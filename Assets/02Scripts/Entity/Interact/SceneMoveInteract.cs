using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveInteract : InteractBase
{
    [SerializeField]
    protected int requireTalk = 0;

    [SerializeField]
    protected int talkCount = 0;

    [Space(10), SerializeField]
    private TalkInteract completeText;
    public int TalkCount
    {
        get => talkCount;
        set
        {
            talkCount = value;

            if (talkCount >= requireTalk)
            {
                if (col != null)
                {
                    col.enabled = true;

                    if (completeText != null)
                    {
                        StartCoroutine("ENd");
                    }
                }
            }
        }
    }
    private IEnumerator ENd()
    {
        while (InvincibleCanvasManager.Inst.Talk_UI.IsON)
        {
            yield return null;
        }
        completeText.Interact();
    }

    [Space(5), SerializeField]
    private SCENE targetScene;

    private Collider col;
    protected void Awake()
    {
        gameObject.tag = "SceneMove";
        col = GetComponent<Collider>();

        talkCount = 0;

        if (requireTalk > 0)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }

    public override void Interact()
    {
        base.Interact();

        GameManager.Inst.SelectScene(targetScene);
    }

    public void ValueChange(string newMessage, SCENE newTargetScene)
    {
        message = newMessage;
        targetScene = newTargetScene;
    }
}
