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

        if (playerCol != null) // ���� �����Ǿ��� ���
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
        Debug.Log(gameObject.name + "��(��) �÷��̾ ������.");

        InvincibleCanvasManager.Inst.Interact_ON(message, this);
    }

    private void InteractEvent_OFF()
    {
        Debug.Log(gameObject.name + "��(��) �÷��̾� ������ ������.");

        InvincibleCanvasManager.Inst.Interact_OFF(this);
    }

    public virtual void Interact()
    {
        Debug.Log(gameObject.name + " ��ȣ�ۿ� ����");

        InvincibleCanvasManager.Inst.Interact_OFF(this);
    }
}
