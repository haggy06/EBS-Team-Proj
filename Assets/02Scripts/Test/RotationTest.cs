using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 dircV = Vector3.zero;
    void Update()
    {
        dircV = target.localPosition.normalized;
                          
        float angle = Mathf.Atan2(dircV.y, dircV.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90, Vector3.up);
    }
}
