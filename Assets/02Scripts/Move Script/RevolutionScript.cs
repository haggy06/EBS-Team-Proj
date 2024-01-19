using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionScript : MonoBehaviour
{
    Vector2 vec;
    private void Update()
    {
        vec.x = Input.GetAxis("Horizontal");
        vec.y = Input.GetAxis("Horizontal");

        transform.localPosition = vec.normalized;
    }
}
