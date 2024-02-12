using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayStartManager : TalkInteract
{
    [Space(10)]

    [SerializeField]
    private DiaryInteract diary;
    [SerializeField]
    private SceneMoveInteract door;

    private void Awake()
    {
        if (GameManager.Inst.Play_Data.curDay == 1)
        {
            talkID = 1001001;


            Interact();
        }
    }
}
