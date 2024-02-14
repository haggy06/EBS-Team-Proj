using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayStartManager : SceneStartInteract
{
    [Space(10)]

    [SerializeField]
    private DiaryInteract diary;
    [SerializeField]
    private SceneMoveInteract door;

    protected override void Event_Day1()
    {
        base.Event_Day1();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(1);
        door.ValueChange("OT 장소로 가기", SCENE.OT);
    }
    protected override void Event_Day2()
    {
        base.Event_Day2();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(2);
        door.ValueChange("강의실로 가기", SCENE.School);
    }
    protected override void Event_Day3()
    {
        GameManager.Inst.SetRoot(GameManager.Inst.CurStress > 0f);

        day3TalkID = GameManager.Inst.EndingRoot ? 3101001 : 3201001;

        base.Event_Day3();

        InvincibleCanvasManager.Inst.Diary_Popup.GetComponent<DiaryPopup>().DiaryChange(3);
        door.ValueChange("강의실로 가기", SCENE.School);
    }
}
