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
    public Fade Fade_Popup => fade; // 이렇게 쓰면 읽기 전용으로 취급됨.

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
        if (Input.GetKeyDown(KeyCode.Escape)) // 일시정지 키를 누를 경우
        {
            if (curPopup != null) // 열린 팝업창이 있었을 경우
            {
                PopupClose();
            }
            else // 열린 팝업창이 없었을 경우
            {
                if (SceneManager.GetActiveScene().buildIndex > 2) // 현재 씬이 Intro(0), Loading(1), Title(2) 씬이 아닐 경우
                {
                    if (fade.MyCanvasGroup.blocksRaycasts) // 페이드 이미지가 있을 경우
                    {
                        Debug.Log("페이드가 끝나지 않아 일시정지를 할 수 없습니다.");
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
                    Debug.Log("Pause Popup을 열 수 없는 씬입니다.");
                }
            }
        }

        if (curBtn != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (curBtn.UpBtn != null) // 이 버튼 위쪽에 버튼이 있을 경우
                {
                    curBtn.DisSelected();
                    SelectBtnChange(curBtn.UpBtn);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (curBtn.DownBtn != null) // 이 버튼 아래쪽에 버튼이 있을 경우
                {
                    curBtn.DisSelected();

                    SelectBtnChange(curBtn.DownBtn);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (curBtn.RightBtn != null) // 이 버튼 오른쪽에 버튼이 있을 경우
                {
                    curBtn.DisSelected();

                    SelectBtnChange(curBtn.RightBtn);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (curBtn.LeftBtn != null) // 이 버튼 왼쪽에 버튼이 있을 경우
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

        if (curPopup != null) // 현재 열려있던 팝업이 있었을 경우
        {
            curPopup.CanvasFadeOut();
            curBtn = null;

            delayedPopupStack.Push(curPopup); // 기존에 열려있던 팝업창은 비활성화 상태로 스택에 쌓아둠
        }
        else // 열려있던 팝업이 없었을 경우
        {
            // popupBackGround.CanvasFadeIn(); // UI 백그라운드 열기
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

        if (delayedPopupStack.TryPop(out curPopup)) // 임시로 닫아뒀던 팝업이 있었을 경우
        { //                                                 └ 가장 마지막에 있던 팝업을 현재 팝업으로 지정
            curPopup.CanvasFadeIn();
            SelectBtnChange(curPopup.FirstBtnNode);
        }
        else // 없었을 경우
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) // 타이틀 씬에 있을 경우
            {
                GameObject.FindWithTag("Player").GetComponent<PopupBase>().CanvasFadeIn(); // 타이틀 씬에선 타이틀 버튼 팝업에만 Player 태그가 있으므로 타이틀 버튼의 FadeIn()을 실행함.
            }
            else if (GameManager.Inst.CurPlayer != null)
            {
                GameManager.Inst.CurPlayer.ControllSwitch(true);
            }

            curPopup = null; // 현재 팝업을 null로 설정

            // popupBackGround.CanvasFadeOut(); // UI 백그라운드 닫기
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
