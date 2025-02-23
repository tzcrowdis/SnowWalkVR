using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ConjuringRitual : MonoBehaviour
{
    public GameObject ghostPrefab; // NOTE may need to use resources or see if prefabs can reference prefabs that aren't in the scene
    GameObject[] ghosts;

    public int ghostCount;
    public float distFromFire;

    public float spawnDist;
    public float spawnPosRandomFactor;

    Vector3[] destinations;

    void Start()
    {
        // get destination based on ghost count and distance from fire
        destinations = new Vector3[ghostCount];
        float angleIncrement = 360 / ghostCount;
        for (int i = 0; i < ghostCount; i++)
        {
            destinations[i] = gameObject.transform.position;
            destinations[i].x += distFromFire * Mathf.Cos(angleIncrement * i * Mathf.Deg2Rad);
            destinations[i].z += distFromFire * Mathf.Sin(angleIncrement * i * Mathf.Deg2Rad);
        }

        // set the center of where all ghosts will spawn
        Vector3 centerPosition = gameObject.transform.forward * spawnDist;
        Vector3 spawnPosition;

        // spawn all ghosts and set their destinations
        ghosts = new GameObject[ghostCount];
        for (int i = 0; i < ghostCount; i++)
        {
            spawnPosition = new Vector3(
                    centerPosition.x + Random.Range(-spawnPosRandomFactor, spawnPosRandomFactor),
                    0,
                    centerPosition.z + Random.Range(-spawnPosRandomFactor, spawnPosRandomFactor)
                );
            ghosts[i] = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            ghosts[i].transform.LookAt(gameObject.transform);
            ghosts[i].GetComponent<GhostBrideBehavior>().agent.destination = destinations[i];
        }
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
