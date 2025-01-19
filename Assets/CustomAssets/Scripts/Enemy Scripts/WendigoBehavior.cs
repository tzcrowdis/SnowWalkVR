using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.AI.Navigation;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class WendigoBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public AnimatorController wendigoAnimController;

    public Animator wendigoAnimator;

    public enum WendigoState
    {
        Chase,
        StalkFar
    }

    public float goonDistance;

    public WendigoState state;

    public bool walking;


    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = false;

        player = GameObject.Find("XR Origin (XR Rig)").transform; // couldn't due from unity editor for some reason
    }

    void Update()
    {
        //TODO - remove when Wendigo spawning logic is done
        if (agent.enabled == false)
            if (FindObjectOfType<NavMeshSurface>().navMeshData != null)
                agent.enabled = true;

        float speed = agent.velocity.magnitude;
        // UnityEngine.Debug.Log(speed);
        if (speed > 2.5f) {
            walking = true;
        }
        else if (walking == false && speed > 0f) {
            walking = true;
        }
        else {
            walking = false;
        }
        wendigoAnimator.SetBool("Walking", walking);


        Vector3 destination;

        switch(state) {
            
            case WendigoState.Chase:
                agent.stoppingDistance = 1;
                destination = (transform.position - player.position).normalized * 6 + player.position;
                agent.destination = destination;
                break;

            case WendigoState.StalkFar:

                agent.stoppingDistance = 2;
                destination = (transform.position - player.position).normalized * goonDistance + player.position;

                //UnityEngine.Debug.Log(destination);

                agent.destination = destination;
                
                break;
        }

    }
}
