using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ESCPopupButtons : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(InvincibleCanvasManager.Inst.PopupClose); // Continue ��ư ����



        transform.GetChild(2).GetComponent<Button>().onClick.AddListener(InvincibleCanvasManager.Inst.GameQuit); // Exit ��ư ����
    }
}
