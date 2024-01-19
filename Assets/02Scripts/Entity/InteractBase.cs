using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractEvent();
        }
    }

    protected virtual void InteractEvent()
    {
        Debug.Log(gameObject.name + "이(가) 플레이어를 감지함.");
    }
}
