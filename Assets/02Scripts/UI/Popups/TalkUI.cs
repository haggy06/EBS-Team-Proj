using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class TalkUI : PopupBase
{
    [Space(5)]

    [SerializeField]
    private ProjectExcel projectExcel;

    [Space(5)]

    [SerializeField]
    private Transform characterSprites;
    [SerializeField]
    private TextMeshProUGUI talkText;

    protected override void OnActive()
    {
        if (GameManager.Inst.CurPlayer != null)
        {
            GameManager.Inst.CurPlayer.ControllSwitch(false);
        }
    }
    protected override void FadeOutFinished()
    {
        if (GameManager.Inst.CurPlayer != null)
        {
            GameManager.Inst.CurPlayer.ControllSwitch(true);
        }
    }

    [Space(10)]
    
    [SerializeField]
    private DB_Text textClass;
    [SerializeField]
    private int textlIndex;
    [SerializeField]
    private string lastTalk = null;
    public void SetTextClass(int targetID)
    {
        CanvasFadeIn();

        for (int i = 0; i < projectExcel.Text.Count; i++)
        {
            if (projectExcel.Text[i].id == targetID)
            {
                textlIndex = i;
                textClass = projectExcel.Text[i];
                break;
            }
        }

        lastTalk = talkText.text = null;

        SettingStart();
    }


    private int lastSprite1 = 0;
    private int lastSprite2 = 0;
    private void SettingStart()
    {
        if (textClass != null)
        {
            StopCoroutine("WriteText_Lerp");
            StartCoroutine("WriteText_Lerp");

            characterSprites.GetChild(lastSprite1).GetComponent<Image>().enabled = false;
            characterSprites.GetChild(lastSprite2).GetComponent<Image>().enabled = false;
            SetCharacter(textClass.npc1, out lastSprite1);
            SetCharacter(textClass.npc2, out lastSprite2);
        }
    }
    private void SettingComplete()
    {
        textClass = null;
        textlIndex = 0;
    }

    private bool writeComplete = false;
    private bool canSkip = false;
    private IEnumerator WriteText_Lerp()
    {
        writeComplete = false;
        canSkip = false;

        for (int i = 0; i < textClass.text.Length; i++)
        {
            if (textClass != null)
            {
                talkText.text += textClass.text[i];

                yield return YieldInstructionCache.WaitFor_FixedUpdate;
                canSkip = true;
            }
        }

        writeComplete = true;
    }

    private void WriteText_Instant()
    {
        talkText.text = lastTalk + textClass.text;

        writeComplete = true;
    }

    private void SetCharacter(int npcIndex, out int spriteIndex)
    {
        if (npcIndex > 0 && npcIndex <= characterSprites.childCount)
        {
            npcIndex--;

            characterSprites.GetChild(npcIndex).GetComponent<Image>().enabled = true;
            spriteIndex = npcIndex;
        }
        else
        {
            spriteIndex = 0;
        }
    }


    private void Update()
    {
        if (textClass != null && Input.GetKeyDown(KeyCode.C)) // 텍스트 클래스가 지정되어 있는 상태에서 C를 눌렀을 경우
        {
            if (writeComplete) // 텍스트 완성이 되어 있었을 경우
            {
                if (textClass.next) // 이어지는 문장이 있을 경우
                {
                    if (!textClass.keep) // 문장이 유지되지 않을 경우
                    {
                        talkText.text = null;

                        lastTalk = null;
                    }
                    else
                    {
                        lastTalk = textClass.text;
                    }

                    textClass = projectExcel.Text[++textlIndex];

                    SettingStart();
                }
                else // 이어지는 문장이 없을 경우
                {
                    CanvasFadeOut();

                }
            }
            else // 텍스트 완성이 안 되어 있었을 경우
            {
                if (canSkip)
                {
                    StopCoroutine("WriteText_Lerp");

                    WriteText_Instant();
                }
            }
        }
    }
}
