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

    float spawnTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        chanting = false;
        spawnTime = 0;
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
            
            if (spawnTime > 1f) // HACK prevent chanting at start up
            {
                animator.SetBool("chanting", true);
                chanting = true;
            }
        }

        spawnTime += Time.deltaTime;
    }
}
