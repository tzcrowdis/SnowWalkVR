using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBrideBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = destination;
    }
}
