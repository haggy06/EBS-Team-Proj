using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : SceneStartInteract
{
    [Space(5)]

    [SerializeField]
    private int day1TalkID_Boy;
    [SerializeField]
    private int day2TalkID_Boy;
    [SerializeField]
    private int day3TalkID_Boy;
    [SerializeField]
    private int day1TalkID_Girl;
    [SerializeField]
    private int day2TalkID_Girl;
    [SerializeField]
    private int day3TalkID_Girl;


    [Space(10)]

    [SerializeField]
    private PeopleInteract boy;
    [SerializeField]
    private PeopleInteract girl;
    [SerializeField]
    private SceneMoveInteract door;


    protected override void Event_Day1()
    {
        base.Event_Day1();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(1);

        boy.TalkID = day1TalkID_Boy;
        if (day1TalkID_Boy == 0)
        {
            boy.enabled = false;
            door.TalkCount++;
        }

        girl.TalkID = day1TalkID_Girl;
        if (day1TalkID_Boy == 0)
        {
            girl.enabled = false;
            door.TalkCount++;
        }
    }
    protected override void Event_Day2()
    {
        base.Event_Day2();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(2);

        boy.TalkID = day2TalkID_Boy;
        if (day2TalkID_Boy == 0)
        {
            boy.enabled = false;
            door.TalkCount++;
        }

        girl.TalkID = day2TalkID_Girl;
        if (day2TalkID_Girl == 0)
        {
            girl.enabled = false;
            door.TalkCount++;
        }
    }
    protected override void Event_Day3()
    {
        base.Event_Day3();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(3);

        boy.TalkID = day3TalkID_Boy;
        if (day3TalkID_Boy == 0)
        {
            boy.enabled = false;
            door.TalkCount++;
        }

        girl.TalkID = day3TalkID_Girl;
        if (day3TalkID_Girl == 0)
        {
            girl.enabled = false;
            door.TalkCount++;
        }
    }
}
