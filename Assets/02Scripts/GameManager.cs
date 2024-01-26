using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public enum Emotion
{
    Normal,
    Smile,
    Happy,
    Panic,
    Sad,
    Scary,
    Suprise,
    Stress,

}
public class GameManager : MonoSingleton<GameManager>
{
    private SCENE saveScene;

    private SCENE currentScene = SCENE.Title;
    public SCENE CurrentScene
    {
        get => currentScene;
        set => currentScene = value;
    }

    #region _Play Data Setting_
    private int playDataIndex = -1;

    [SerializeField]
    private PlayData playData;
    public PlayData Play_Data => playData;
    #endregion
    private Emotion curEmotion;
    public Emotion CurEmotion
    {
        set => curEmotion = value;

        get => curEmotion;
    }

    [SerializeField, Range(-1f, 1f)]
    private float curStress = 0f; // 백분율
    public float CurStress => curStress;

    private new void Awake()
    {
        base.Awake();

        saveScene = (SCENE)PlayerPrefs.GetInt("LastScene");

        curStress = PlayerPrefs.GetFloat("StressGauge");
    }

    public void SceneLoadComplete()
    {
        StartCoroutine("NextSceneLoaded");
    }
    private IEnumerator NextSceneLoaded()
    {
        yield return null;

        Debug.Log("새로운 씬 실행 : " + SceneManager.GetActiveScene().buildIndex);

        InvincibleCanvasManager.Inst.NextSceneLoaded();

        if (SceneManager.GetActiveScene().buildIndex > 2) // 현재 씬이 Intro(0), Loading(1), Title(2) 씬이 아닐 경우
        {
            Debug.Log("게임 씬 로드됨");

            InvincibleCanvasManager.Inst.PlayerUI_On();

            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                /*
                if (player.TryGetComponent<PlayerController>(out playerController))
                {
                    playerController.SetAppearPos(outForm, startPos);
                }
                */
            }
            else
            {
                Debug.LogError("현재 씬에 플레이어가 없습니다");
            }
        }
    }

    public void SelectScene(SCENE selectedScene, Vector2 newStartPos)
    {
        currentScene = selectedScene;

        InvincibleCanvasManager.Inst.Fade_Popup.StartFade(FadeMode.LoadScene);
    }

    public void StressUP(float stressAmount)
    {
        curStress = Mathf.Clamp(curStress + stressAmount, -1f, 1f);
        InvincibleCanvasManager.Inst.Player_UI.StressRenewal_Lerp();

        PlayerPrefs.SetFloat("StressGauge", curStress);
    }
    public void StressDOWN(float stressAmount)
    {
        curStress = Mathf.Clamp(curStress - stressAmount, -1f, 1f);
        InvincibleCanvasManager.Inst.Player_UI.StressRenewal_Lerp();

        PlayerPrefs.SetFloat("StressGauge", curStress);
    }

    public void SelectScene(SCENE selectedScene)
    {
        currentScene = selectedScene;

        InvincibleCanvasManager.Inst.Fade_Popup.StartFade(FadeMode.LoadScene);
    }

    public void GoLoadingScene()
    {
        SceneManager.LoadScene((int)SCENE.Loading);
    }

    public void GameQuit()
    {
        //FileSaveLoader.Inst.SaveData(Save_DATA.EctData.ToString(), JsonUtility.ToJson(ectData));

        if (playDataIndex != -1) // 열려있던 게임데이터가 있었을 경우
        {

        }
        Application.Quit(); // 어플리케이션 종료

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
