using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class InvincibleCanvasManager : MonoSingleton<InvincibleCanvasManager>
{
    #region _Popups_
    [Space(10f), SerializeField]
    private Fade fade;
    public Fade Fade_Popup => fade; // �̷��� ���� �б� �������� ��޵�.

    [SerializeField]
    private PlayerUI playerUI;
    public PlayerUI Player_UI => playerUI;

    [SerializeField]
    private TalkUI talkUI;
    public TalkUI Talk_UI => talkUI;

    [SerializeField]
    private ChoicePopup choicePopup;
    public ChoicePopup Choice_Popup => choicePopup;

    [SerializeField]
    private PopupBase interactAlert;
    public PopupBase InteractAlert => interactAlert;

    [SerializeField]
    private PopupBase diaryPopup;
    public PopupBase DiaryPopup => diaryPopup;

    [SerializeField]
    private PopupBase escPopup;
    public PopupBase ESC_Popup => escPopup;
    #endregion
    private new void Awake()
    {
        base.Awake();

        #region _Fade Component_
        fade = transform.Find("Fade").GetComponent<Fade>();
        fade.InitInfo();

        fade.CanvasHide();
        #endregion

        #region _PlayerUI Component_
        playerUI = transform.Find("Player UI").GetComponent<PlayerUI>();
        playerUI.InitInfo();

        playerUI.CanvasHide();
        #endregion

        #region _DiaryPopup Component_
        diaryPopup = transform.Find("Diary Popup").GetComponent<PopupBase>();
        diaryPopup.InitInfo();

        diaryPopup.CanvasHide();
        #endregion

        #region _ChoicePopup Component_
        choicePopup = transform.Find("Choice Buttons").GetComponent<ChoicePopup>();
        choicePopup.InitInfo();

        choicePopup.CanvasHide();
        #endregion

        #region _InteractAlert Component_
        interactAlert = transform.Find("Interact Alert").GetComponent<PopupBase>();
        interactAlert.InitInfo();

        interactAlert.CanvasHide();
        #endregion

        #region _TalkUI Component_
        talkUI = transform.Find("Talk UI").GetComponent<TalkUI>();
        talkUI.InitInfo();

        talkUI.CanvasHide();
        #endregion

        #region _ESC Popup Component_
        escPopup = transform.Find("ESC Popup").GetComponent<PopupBase>();
        escPopup.InitInfo();

        escPopup.CanvasHide();
        #endregion
    }

    private InteractBase curActTarget;
    public InteractBase CurActTarget => curActTarget;
    public void Interact_ON(string message, InteractBase curTarget)
    {
        curActTarget = curTarget;

        interactAlert.GetComponent<TextMeshProUGUI>().text = message;

        interactAlert.CanvasFadeIn();
    }
    public void Interact_OFF(InteractBase outTarget)
    {
        if (curActTarget == outTarget)
        {
            curActTarget = null;
        }

        interactAlert.CanvasFadeOut();
    }

    public void NextSceneLoaded()
    {
        delayedPopupStack.Clear();

        fade.CanvasShow();
        fade.CanvasFadeOut();
    }
    public void PlayerUI_On()
    {
        playerUI.CanvasShow();

        playerUI.StressRenewal_Instant();
    }

    private Stack<PopupBase> delayedPopupStack = new Stack<PopupBase>();
    public Stack<PopupBase> DelayedPopupStack => delayedPopupStack;

    private PopupBase curPopup;
    public PopupBase CurPopup => curPopup;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // �Ͻ����� Ű�� ���� ���
        {
            if (curPopup != null) // ���� �˾�â�� �־��� ���
            {
                PopupClose();
            }
            else // ���� �˾�â�� ������ ���
            {
                if (SceneManager.GetActiveScene().buildIndex > 2) // ���� ���� Intro(0), Loading(1), Title(2) ���� �ƴ� ���
                {
                    if (fade.MyCanvasGroup.blocksRaycasts) // ���̵� �̹����� ���� ���
                    {
                        Debug.Log("���̵尡 ������ �ʾ� �Ͻ������� �� �� �����ϴ�.");
                    }
                    else
                    {
                        if (!talkUI.PopupOpened)
                        {
                            PopupOpen(escPopup);
                            GameManager.Inst.GamePause(true);
                        }
                    }
                }
                else
                {
                    Debug.Log("Pause Popup�� �� �� ���� ���Դϴ�.");
                }
            }
        }

        if (curBtn != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (curBtn.UpBtn != null) // �� ��ư ���ʿ� ��ư�� ���� ���
                {
                    curBtn.DisSelected();
                    SelectBtnChange(curBtn.UpBtn);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (curBtn.DownBtn != null) // �� ��ư �Ʒ��ʿ� ��ư�� ���� ���
                {
                    curBtn.DisSelected();

                    SelectBtnChange(curBtn.DownBtn);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (curBtn.RightBtn != null) // �� ��ư �����ʿ� ��ư�� ���� ���
                {
                    curBtn.DisSelected();

                    SelectBtnChange(curBtn.RightBtn);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (curBtn.LeftBtn != null) // �� ��ư ���ʿ� ��ư�� ���� ���
                {
                    curBtn.DisSelected();

                    SelectBtnChange(curBtn.LeftBtn);
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                curBtn.BtnClick();
            }
        }
    }

    #region _Popup Controll_
    public void PopupOpen(PopupBase newPopup)
    {
        Debug.Log("Popup Open : " + newPopup.gameObject.name);

        if (curPopup != null) // ���� �����ִ� �˾��� �־��� ���
        {
            curPopup.CanvasFadeOut();
            curBtn = null;

            delayedPopupStack.Push(curPopup); // ������ �����ִ� �˾�â�� ��Ȱ��ȭ ���·� ���ÿ� �׾Ƶ�
        }
        else // �����ִ� �˾��� ������ ���
        {
            // popupBackGround.CanvasFadeIn(); // UI ��׶��� ����
        }

        newPopup.CanvasFadeIn();
        SelectBtnChange(newPopup.FirstBtnNode);

        curPopup = newPopup;
    }

    public void PopupClose()
    {
        Debug.Log("Popup Close");

        curPopup.CanvasFadeOut();
        curBtn = null;

        if (curPopup == escPopup)
        {
            GameManager.Inst.GamePause(false);
        }

        if (delayedPopupStack.TryPop(out curPopup)) // �ӽ÷� �ݾƵ״� �˾��� �־��� ���
        { //                                                 �� ���� �������� �ִ� �˾��� ���� �˾����� ����
            curPopup.CanvasFadeIn();
            SelectBtnChange(curPopup.FirstBtnNode);
        }
        else // ������ ���
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) // Ÿ��Ʋ ���� ���� ���
            {
                GameObject.FindWithTag("Player").GetComponent<PopupBase>().CanvasFadeIn(); // Ÿ��Ʋ ������ Ÿ��Ʋ ��ư �˾����� Player �±װ� �����Ƿ� Ÿ��Ʋ ��ư�� FadeIn()�� ������.
            }
            else if (GameManager.Inst.CurPlayer != null)
            {
                GameManager.Inst.CurPlayer.ControllSwitch(true);
            }

            curPopup = null; // ���� �˾��� null�� ����

            // popupBackGround.CanvasFadeOut(); // UI ��׶��� �ݱ�
        }
    }
    #endregion


    public void AllPopupClose()
    {
        if (curPopup != null)
        {
            curPopup.CanvasFadeOut();
            curPopup = null;
        }

        curBtn = null;
    }

    public void GoToMain()
    {
        AllPopupClose();

        GameManager.Inst.SelectScene(SCENE.Title);
    }

    public void GameQuit()
    {
        fade.StartFade(FadeMode.GameQuit);
    }

    /*
    public void KeySettingReset(Transform content_settingButtons)
    {
        GameManager.Inst.ResetInputData();

        for (int i = 0; i < content_settingButtons.childCount; i++)
        {
            content_settingButtons.GetChild(i).GetComponent<KeySettingButton>().WriteCurrentKeycode();
        }
    }
    */

    [SerializeField]
    private ButtonNode curBtn;
    public ButtonNode CurBtn => curBtn;

    public void SelectBtnChange(ButtonNode newNode)
    {
        if (curBtn != null)
        {
            curBtn.DisSelected();
        }

        curBtn = newNode;

        if (curBtn != null)
        {
            curBtn.Selected();
        }
    }
}
