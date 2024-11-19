using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WendigoBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = player.position;
    }

    // Fix for awkward movement between planes:
    // https://www.reddit.com/r/Unity3D/comments/f9i7wv/my_agents_walk_faster_when_going_through/
}
