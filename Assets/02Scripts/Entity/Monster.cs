using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private NavMeshAgent nav;
    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    public void FollowPlayer(bool b)
    {
        StopCoroutine("TargetTracking");

        if (b)
        {
            StartCoroutine("TargetTracking");
        }
    }

    public void Forgive()
    {
        nav.SetDestination(Vector3.zero);
        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator TargetTracking()
    {
        while (true)
        {
            nav.SetDestination(target.position);

            yield return null;
        }
    }
}
