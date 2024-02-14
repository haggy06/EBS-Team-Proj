using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartInteract : TalkInteract
{
    [Space(10)]

    [SerializeField]
    protected int day1TalkID = 0;
    [SerializeField]
    protected int day2TalkID = 0;
    [SerializeField]
    protected int day3TalkID = 0;

    private void Awake()
    {
        switch (GameManager.Inst.Play_Data.curDay)
        {
            case 1:
                Event_Day1();
                break;
            case 2:
                Event_Day2();
                break;
            case 3:
                Event_Day3();
                break;
        }
    }

    protected virtual void Event_Day1()
    {
        talkID = day1TalkID;
        Interact();
    }
    protected virtual void Event_Day2()
    {
        talkID = day2TalkID;
        Interact();
    }
    protected virtual void Event_Day3()
    {
        talkID = day3TalkID;
        Interact();
    }
}
