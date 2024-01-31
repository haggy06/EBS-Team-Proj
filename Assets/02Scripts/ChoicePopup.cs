using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePopup : PopupBase
{
    [Space(5)]

    [SerializeField]
    private ChoiceButton btn1;
    [SerializeField]
    private ChoiceButton btn2;
    [SerializeField]
    private ChoiceButton btn3;

    public void SetChoice(string choice1)
    {
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(false);
        btn3.gameObject.SetActive(false);

        btn1.UpBtn = null;
        btn1.DownBtn = null;

        btn1.Text.text = choice1;

        CanvasFadeIn();
    }
    public void SetChoice(string choice1, string choice2)
    {
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(false);

        btn1.UpBtn = btn2;
        btn1.DownBtn = btn2;
        btn2.UpBtn = btn1;
        btn2.DownBtn = btn1;

        btn1.Text.text = choice1;
        btn2.Text.text = choice2;

        CanvasFadeIn();
    }
    public void SetChoice(string choice1, string choice2, string choice3)
    {
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(true);

        btn1.UpBtn = btn3;
        btn1.DownBtn = btn2;
        btn2.UpBtn = btn1;
        btn2.DownBtn = btn3;
        btn3.UpBtn = btn2;
        btn3.DownBtn = btn1;

        btn1.Text.text = choice1;
        btn2.Text.text = choice2;
        btn3.Text.text = choice3;

        CanvasFadeIn();
    }
}
