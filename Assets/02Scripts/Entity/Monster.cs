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

    private void FixedUpdate()
    {
        nav.SetDestination(target.position);
    }
}
