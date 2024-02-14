using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : SceneStartInteract
{
    [SerializeField]
    private Monster monster;

    [SerializeField]
    private BGMPlayer bgmPlayer;
    [SerializeField]
    private AudioClip yesMonster;
    protected override void Event_Day1()
    {
        base.Event_Day1();

        monster.gameObject.SetActive(false);
    }
    protected override void Event_Day2()
    {
        base.Event_Day2();

        monster.gameObject.SetActive(true);
        StartCoroutine("MonsterWait");
        bgmPlayer.bgm = yesMonster;
    }
    protected override void Event_Day3()
    {
        day3TalkID = GameManager.Inst.EndingRoot ? 3101002 : 3201002;

        base.Event_Day3();

        monster.FollowPlayer(false);
    }

    private IEnumerator MonsterWait()
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        yield return YieldInstructionCache.WaitFor_FixedUpdate;

        while (!player.CanCotroll)
        {
            yield return YieldInstructionCache.WaitFor_FixedUpdate;
        }

        monster.FollowPlayer(true);
    }
}
