using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    [SerializeField]
    private float StressPerSec = -5f;

    [SerializeField]
    private int touchedLamp = 0;

    private void Start()
    {
        RandomLampON();

        LampTouch(true);
        LampTouch(false);
    }

    [ContextMenu("Lamp Renewal")]
    private void RandomLampON()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<StreetLamp>().LightON(Random.Range(0, 2) == 1);
        }
    }

    public void LampTouch(bool enter)
    {
        if (enter)
        {
            touchedLamp++;
        }
        else
        {
            touchedLamp--;

            if (touchedLamp < 1)
            {
                StopCoroutine("Darkness");
                StartCoroutine("Darkness");
            }
        }
    }

    private IEnumerator Darkness()
    {
        while (touchedLamp < 1)
        {
            GameManager.Inst.StressChange_Instant(StressPerSec * Time.deltaTime);

            yield return null;
        }
    }
}
