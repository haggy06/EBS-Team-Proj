using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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
    private PopupBase popupBackGround;
    public PopupBase Background_Popup => popupBackGround;
    #endregion
    private new void Awake()
    {
        base.Awake();

        #region _Fade Component_
        fade = transform.Find("Fade").GetComponent<Fade>();
        fade.InitInfo();

        fade.CanvasShow();
        #endregion

        #region _PlayerUI Component_
        playerUI = transform.Find("Player UI").GetComponent<PlayerUI>();
        playerUI.InitInfo();

        playerUI.CanvasHide();
        #endregion
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
            }/*
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
                        GameManager.Inst.GamePause_ON();
                    }
                }
                else
                {
                    Debug.Log("Pause Popup�� �� �� ���� ���Դϴ�.");
                }
            }*/
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
            popupBackGround.CanvasFadeIn(); // UI ��׶��� ����
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

            curPopup = null; // ���� �˾��� null�� ����

            popupBackGround.CanvasFadeOut(); // UI ��׶��� �ݱ�
        }
    }
    #endregion


    public void AllPopupClose()
    {
        if (curPopup != null)
        {
            curPopup.CanvasFadeOut();
            curPopup = null;

            popupBackGround.CanvasFadeOut(); // UI ��׶��� �ݱ�
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

        curBtn.Selected();
    }
}
