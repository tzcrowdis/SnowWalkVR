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

    public AnimatorController wendigoAnimController;
    public Animator wendigoAnimator;

    public bool walking;
    public float walkSpeed;
    public float sprintSpeed;

    Transform prey;
    NavMeshObstacle preyObstacle;
    float preyObstacleRadius;

    enum WendigoState
    {
        Spawning,
        Circling,
        TestCharges,
        Charge,
        Flee
    }
    WendigoState state = WendigoState.Circling;

    public float circlingRadius;
    public Transform circlingDestination;

    int testChargeCount = 0;
    public int testChargeCountLimit;
    public float testChargeDistance;

    public float attackDistance;

    Vector3 fleeDestination;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = false;

        prey = GameObject.Find("XR Origin (XR Rig)").transform; // couldn't do from unity editor for some reason
        preyObstacle = prey.GetComponent<NavMeshObstacle>();
        preyObstacleRadius = preyObstacle.radius;

        // testing 
        state = WendigoState.Circling;
    }

    void Update()
    {
        //TODO - remove when Wendigo spawning logic is done
        if (agent.enabled == false)
            if (FindObjectOfType<NavMeshSurface>().navMeshData != null)
                agent.enabled = true;

        /*
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
        */

        switch (state)
        {
            case WendigoState.Spawning:
                Spawning();
                break;
            case WendigoState.Circling:
                Circling();
                break;
            case WendigoState.TestCharges:
                TestCharges();
                break;
            case WendigoState.Charge:
                Charge();
                break;
            case WendigoState.Flee:
                Flee();
                break;
        }
    }

    void Spawning()
    {
        agent.speed = 0f;

        // TODO if spawning animation is complete ->
        preyObstacle.radius = circlingRadius;
        state = WendigoState.Circling;
    }

    void Circling()
    {
        // walk towards position behind players head
        agent.destination = circlingDestination.position;
        agent.stoppingDistance = 1f;

        wendigoAnimator.SetBool("Walking", true);
        agent.speed = walkSpeed;

        // if behind player -> charge or test charge 
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            preyObstacle.radius = preyObstacleRadius;

            if (testChargeCount >= testChargeCountLimit)
                state = WendigoState.Charge;
            else
                state = WendigoState.TestCharges;
        }
    }

    void TestCharges()
    {
        agent.destination = prey.position;
        agent.stoppingDistance = testChargeDistance;

        // TODO sprint animation
        agent.speed = sprintSpeed;

        // if within test charge distance -> flee
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            state = WendigoState.Flee;
        }
    }

    void Charge()
    {
        agent.destination = prey.position;
        agent.stoppingDistance = attackDistance;

        // TODO sprint animation
        agent.speed = sprintSpeed;

        // if within attack distance -> kill prey
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            // TODO kill
        }
    }

    void Flee()
    {
        if (fleeDestination == null)
            fleeDestination = (transform.position - prey.position).normalized * circlingRadius + transform.position;

        agent.destination = fleeDestination;
        agent.stoppingDistance = 1f;

        // TODO sprint animation
        agent.speed = sprintSpeed;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            fleeDestination = new Vector3();
            preyObstacle.radius = circlingRadius;
            state = WendigoState.Circling;
        }
    }

    public void HitBySnowball()
    {
        agent.speed = 0f;

        // play animation? like shaking off

        state = WendigoState.Flee;
    }
}
