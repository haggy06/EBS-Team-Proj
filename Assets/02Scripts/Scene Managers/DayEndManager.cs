using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEndManager : SceneStartInteract
{
    [Space(5)]

    [SerializeField]
    protected int day1TalkID_Cat = 0;
    [SerializeField]
    protected int day2TalkID_Cat = 0;
    [SerializeField]
    protected int day3TalkID_Cat = 0;

    [Space(10)]

    [SerializeField]
    private DiaryInteract diary;
    [SerializeField]
    private BedInteract bed;
    [SerializeField]
    private PeopleInteract cat;

    [Space(15)]

    [SerializeField]
    private BGMPlayer bgmPlayer;
    [SerializeField]
    private AudioClip badEnd;
    [SerializeField]
    private AudioClip happyEnd;

    protected override void Event_Day1()
    {
        base.Event_Day1();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(2);
        cat.TalkID = day1TalkID_Cat;
    }
    protected override void Event_Day2()
    {
        base.Event_Day2();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(3);
        cat.TalkID = day2TalkID_Cat;
    }
    protected override void Event_Day3()
    {
        day3TalkID = GameManager.Inst.EndingRoot ? 3101031 : 3201023;

        base.Event_Day3();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(3);
        cat.TalkID = day3TalkID_Cat;
        bed.gameObject.SetActive(false);
    }
}
