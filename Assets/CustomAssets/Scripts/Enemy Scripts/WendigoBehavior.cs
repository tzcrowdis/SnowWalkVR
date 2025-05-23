using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting.FullSerializer;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class WendigoBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    NavMeshSurface surface;

    //public AnimatorController wendigoAnimController;
    public Animator wendigoAnimator;

    public Renderer skin;

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
    bool fleeDestSet = false;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        surface = GameObject.Find("Wendigo Navmesh Surface").GetComponent<NavMeshSurface>();
        //agent.enabled = false;

        skin.enabled = false;

        prey = GameObject.Find("XR Origin (XR Rig)").transform; // couldn't do from unity editor for some reason
        preyObstacle = prey.GetComponent<NavMeshObstacle>();
        preyObstacleRadius = preyObstacle.radius;

        // testing 
        state = WendigoState.Circling;
        preyObstacle.radius = circlingRadius;
        surface.BuildNavMesh();
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

        Debug.Log(state);
    }

    void Spawning()
    {
        agent.speed = 0f;

        // TODO if spawning animation is complete ->
        preyObstacle.radius = circlingRadius;
        surface.BuildNavMesh();
        state = WendigoState.Circling;
    }

    void Circling()
    {
        // TESTING
        preyObstacle.radius = preyObstacleRadius;
        skin.enabled = false;
        agent.speed = sprintSpeed * (testChargeCount + 1) * 2;
        agent.stoppingDistance = 5f;


        // walk towards position behind players head
        agent.destination = prey.position - circlingRadius * prey.forward;
        //agent.stoppingDistance = 0.1f;

        wendigoAnimator.SetBool("Walking", true);
        //agent.speed = sprintSpeed;

        // if behind player -> charge or test charge 
        if (WendigoAtDestination())
        {
            preyObstacle.radius = preyObstacleRadius;
            surface.BuildNavMesh();

            if (testChargeCount >= testChargeCountLimit)
                state = WendigoState.Charge;
            else
                state = WendigoState.TestCharges;
        }
    }

    void TestCharges()
    {
        skin.enabled = true;
        
        agent.destination = prey.position;
        agent.stoppingDistance = testChargeDistance;

        // TODO sprint animation
        agent.speed = sprintSpeed;

        // if within test charge distance -> flee
        if (WendigoAtDestination())
        {
            testChargeCount++;
            state = WendigoState.Flee;
        }
    }

    void Charge()
    {
        skin.enabled = true;
        
        agent.destination = prey.position;
        agent.stoppingDistance = attackDistance;

        // TODO sprint animation
        agent.speed = sprintSpeed;

        // if within attack distance -> kill prey
        if (WendigoAtDestination())
        {           
            Debug.Log("KILL");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    void Flee()
    {
        if (!fleeDestSet)
        {
            Vector3 fleeDirection = (transform.position - prey.position).normalized + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
            fleeDestination = fleeDirection.normalized * circlingRadius * 1.1f + prey.position;
            fleeDestSet = true;
        }  

        agent.destination = fleeDestination;
        agent.stoppingDistance = 0.1f;

        // TODO sprint animation
        agent.speed = sprintSpeed;

        if (WendigoAtDestination())
        {
            fleeDestSet = false;
            preyObstacle.radius = circlingRadius;
            surface.BuildNavMesh();
            state = WendigoState.Circling;
        }
    }

    bool WendigoAtDestination()
    {
        return !agent.pathPending && agent.remainingDistance < agent.stoppingDistance;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Snowball")
            return;

        // play animation? like shaking off

        state = WendigoState.Flee;
    }
}
