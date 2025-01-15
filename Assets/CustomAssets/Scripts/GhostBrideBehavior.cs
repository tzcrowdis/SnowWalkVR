using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class GhostBrideBehavior : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public GameObject bonfire;

    [HideInInspector]
    public bool chanting;

    Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        chanting = false;
    }

    void Update()
    {
        // HACK conjuring ritual script wasn't setting bonfire object
        if (bonfire == null)
            bonfire = GameObject.Find("Bonfire(Clone)");
        
        transform.LookAt(agent.destination);

        // reached location -> look at fire and start chanting
        if (agent.remainingDistance < agent.stoppingDistance & agent.velocity.sqrMagnitude == 0f)
        {
            transform.LookAt(bonfire.transform);
            
            animator.SetBool("chanting", true);
            chanting = true;
        }
    }
}
