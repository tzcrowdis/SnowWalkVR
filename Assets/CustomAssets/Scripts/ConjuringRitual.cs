using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuringRitual : MonoBehaviour
{
    public GameObject ghostPrefab; // NOTE may need to use resources or see if prefabs can reference prefabs that aren't in the scene
    GameObject[] ghosts;

    public int ghostCount;
    public float distFromFire;

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

        // spawn all ghosts and set their destinations
        ghosts = new GameObject[ghostCount];
        Vector3 position = Vector3.zero;
        for (int i = 0; i < ghostCount; i++)
        {
            ghosts[i] = Instantiate(ghostPrefab, position, Quaternion.identity);
            ghosts[i].transform.LookAt(gameObject.transform);
            ghosts[i].GetComponent<GhostBrideBehavior>().agent.destination = destinations[i];
            ghosts[i].GetComponent<GhostBrideBehavior>().bonfire = gameObject;
        }
    }

    void Update()
    {
        // check for all ghosts in position
        for (int i = 0; i < ghostCount; i++)
        {
            if (!ghosts[i].GetComponent<GhostBrideBehavior>().chanting)
                break;

            if (i == ghostCount - 1)
            {
                // TODO start next step of ritual...
            }
        }
    }
}