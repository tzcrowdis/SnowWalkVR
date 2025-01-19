using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class WendigoBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    public AnimatorController wendigoAnimController;
    public Animator wendigoAnimator;
    
    public enum WendigoState
    {
        Chase,
        StalkFar
    }
    public float goonDistance;
    public WendigoState state;
    private bool walking;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = false;
        
    }

    void Update()
    {
        //TODO - remove when Wendigo spawning logic is done
        if (agent.enabled == false)
            if (FindObjectOfType<NavMeshSurface>().navMeshData != null)
                agent.enabled = true;

        //Logic to make walking to idle animation change more natural
        float speed = agent.velocity.magnitude;
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

        //Main behavior state machine
        switch(state) {
            
            case WendigoState.Chase:
                agent.stoppingDistance = 1;
                destination = (transform.position - player.position).normalized * 6 + player.position;
                agent.destination = destination;

                //Once he is at destination, wait for player to look

                break;

            case WendigoState.StalkFar:

                agent.stoppingDistance = 2;
                destination = (transform.position - player.position).normalized * goonDistance + player.position;

                //Debug.Log(destination);

                agent.destination = destination;
                
                break;
        }

        WendigoInView();

    }

    bool WendigoInView() {
        Vector3 playerRot = player.GetComponentInChildren<Camera>().transform.eulerAngles;
        Vector3 playerPos = player.GetComponentInChildren<Camera>().transform.position;
        
        Vector3 dirFromPlayer = (transform.position - playerPos).normalized;
        Debug.DrawRay(playerPos, dirFromPlayer * 100, Color.red);

        Debug.Log(dirFromPlayer);

        return false;
    }
}
