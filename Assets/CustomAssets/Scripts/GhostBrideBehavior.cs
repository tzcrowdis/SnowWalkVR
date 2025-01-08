using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class GhostBrideBehavior : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    public GameObject bonfire;
    public bool chanting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // TODO enable walking animation

        chanting = false;
    }

    void Update()
    {
        transform.LookAt(agent.destination);

        // reached location -> look at fire and start chanting
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                transform.LookAt(bonfire.transform);
                // TODO change animation

                chanting = true;
            }
        }
    }
}
