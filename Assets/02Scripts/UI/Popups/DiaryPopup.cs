using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class DiaryPopup : PopupBase
{
    public void DiaryChange(int day)
    {
        transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(Path.Combine("Diary", day.ToString()));
    }
}
