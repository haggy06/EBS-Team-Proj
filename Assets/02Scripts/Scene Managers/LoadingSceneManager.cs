using System.Collections;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private float minimumLatency = 1f;

    private void Awake()
    {
        InvincibleCanvasManager.Inst.Fade_Popup.CanvasHide();
        InvincibleCanvasManager.Inst.Player_UI.CanvasHide();

        StartCoroutine("LoadAsyncScene");
    }

    private IEnumerator LoadAsyncScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameManager.Inst.CurrentScene.ToString());

        asyncOperation.allowSceneActivation = false;

        Debug.Log("씬 로드 일시정지");

        yield return YieldInstructionCache.WaitForSeconds(minimumLatency);

        Debug.Log("씬 로드 일시정지 해제");

        while (!Mathf.Approximately(asyncOperation.progress, 0.9f))
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;

        InvincibleCanvasManager.Inst.Fade_Popup.CanvasShow();
        GameManager.Inst.SceneLoadComplete();
    }
}
