using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InteractBase : MonoBehaviour
{
    /*
    [SerializeField]
    private float sensorRadius = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }
    private Collider[] playerCol;
    private void FixedUpdate()
    {
        playerCol = Physics.OverlapSphere(transform.position, sensorRadius, 1 << (int)LAYER.Player);

        if (playerCol != null) // 뭔가 감지되었을 경우
        {

        }
    }
    */
    [SerializeField]
    protected string message = "Press C To Interact";

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LookingPos"))
        {
            InteractEvent_ON();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LookingPos"))
        {
            InteractEvent_OFF();
        }
    }

    private void InteractEvent_ON()
    {
        Debug.Log(gameObject.name + "이(가) 플레이어를 감지함.");

        InvincibleCanvasManager.Inst.Interact_ON(message, this);
    }

    private void InteractEvent_OFF()
    {
        Debug.Log(gameObject.name + "이(가) 플레이어 감지를 종료함.");

        InvincibleCanvasManager.Inst.Interact_OFF(this);
    }

    public virtual void Interact()
    {
        Debug.Log(gameObject.name + " 상호작용 시작");

        InvincibleCanvasManager.Inst.Interact_OFF(this);
    }
}
