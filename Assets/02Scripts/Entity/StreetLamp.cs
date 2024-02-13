using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamp : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObj;
    [SerializeField]
    private Collider col;

    private void Awake()
    {
        lightObj = transform.GetChild(0).gameObject;
        col = GetComponent<Collider>();
    }

    public void LightON(bool on_off)
    {
        lightObj.SetActive(on_off);
        col.enabled = on_off;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponentInParent<LampManager>().LampTouch(true);

            Debug.Log("램프 들어감");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponentInParent<LampManager>().LampTouch(false);

            Debug.Log("램프 나옴");
        }
    }
}
