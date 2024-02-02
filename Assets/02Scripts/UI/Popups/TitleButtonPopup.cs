using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TitleButtonPopup : PopupBase
{
    [SerializeField]
    private SCENE moveScene;

    private string jsonData = null;
    private void Awake()
    {
        InitInfo();

        Button btn;

        btn = transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(GameManager.Inst.NewData);

        btn = transform.GetChild(1).GetComponent<Button>();
        btn.onClick.AddListener(GameManager.Inst.LoadData);
        btn.enabled = FileSaveLoader.Inst.TryLoadData(SaveData.PlayData.ToString(), out jsonData);

        btn = transform.GetChild(2).GetComponent<Button>();
        btn.onClick.AddListener(() => InvincibleCanvasManager.Inst.Fade_Popup.StartFade(FadeMode.GameQuit));

        InvincibleCanvasManager.Inst.SelectBtnChange(transform.GetChild(3).GetComponent<ButtonNode>());
    }
}

/*
#region _Input Data Import_
        if (FileSaveLoader.Inst.TryLoadData(Save_DATA.SettingData.ToString(), out jsonData))
        { // 파일 불러오기에 성공했을 경우
            settingData = JsonUtility.FromJson<SettingData>(jsonData);
        }
        else
        { // 파일 불러오기에 실패했을 경우
            FileSaveLoader.Inst.SaveData(Save_DATA.SettingData.ToString(), JsonUtility.ToJson(new SettingData()));

            settingData = new SettingData();
        }
        #endregion
*/