using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class ConjuringRitual : MonoBehaviour
{
    public GameObject ghostPrefab; // NOTE may need to use resources or see if prefabs can reference prefabs that aren't in the scene
    GameObject[] ghosts;

    public int ghostCount;
    public float distFromFire;

    public float spawnDist;
    public float spawnPosRandomFactor;

    Vector3[] destinations;

    bool playerObserving;
    bool allChanting;
    float conjuringTime;
    public float ritualCompleteTime;

    void Start()
    {
        // get destination based on ghost count and distance from fire
        destinations = new Vector3[ghostCount];
        float angleIncrement = 360 / ghostCount;
        for (int i = 0; i < ghostCount; i++)
        {
            destinations[i] = transform.position;
            destinations[i].x += distFromFire * Mathf.Cos(angleIncrement * i * Mathf.Deg2Rad);
            destinations[i].z += distFromFire * Mathf.Sin(angleIncrement * i * Mathf.Deg2Rad);
        }

        // set the center of where all ghosts will spawn
        Vector3 centerPosition = transform.forward * spawnDist + transform.position;
        Vector3 spawnPosition;

        // spawn all ghosts and set their destinations
        ghosts = new GameObject[ghostCount];
        for (int i = 0; i < ghostCount; i++)
        {
            RandomSpawnPointOnMesh(centerPosition, spawnPosRandomFactor, out spawnPosition);

            ghosts[i] = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            ghosts[i].transform.LookAt(transform);
            ghosts[i].GetComponent<GhostBrideBehavior>().agent.destination = destinations[i];
        }

        // early exit variables
        allChanting = false;
        playerObserving = false;
        conjuringTime = 0;
    }

    bool RandomSpawnPointOnMesh(Vector3 center, float radius, out Vector3 result)
    {
        // find valid spawn point on the mesh for the ghosts
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        // no valid point found then spawn in fire
        result = transform.position;
        return false;
    }

    private void Update()
    {
        // early exit of act 2
        if (conjuringTime > ritualCompleteTime)
        {
            ProgressTracker world = GameObject.Find("World").GetComponent<ProgressTracker>();
            world.gameTime = world.actThreeStartTime;
        }

        allChanting = true;
        foreach (GameObject ghost in ghosts)
        {
            if (!ghost.GetComponent<GhostBrideBehavior>().chanting)
            {
                allChanting = false;
                break;
            }
        }

        if (allChanting & playerObserving)
            conjuringTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerObserving = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerObserving = false;
    }

    void OnDestroy()
    {
        // leave behind the pieces
        GameObject kindling = transform.GetChild(1).gameObject;
        kindling.transform.parent = null;

        GameObject sticks = transform.GetChild(0).gameObject;
        sticks.transform.parent = null;

        ParticleSystem fire = sticks.transform.GetChild(0).GetComponent<ParticleSystem>();
        fire.transform.parent = null;
        fire.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        Destroy(sticks);
    }
}
