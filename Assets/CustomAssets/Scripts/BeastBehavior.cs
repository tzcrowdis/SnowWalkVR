using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastBehavior : MonoBehaviour
{
    public float spawnDistance;
    float spawnAngle;
    
    float timeBetweenAppearances;
    public float minTimeToSpawn;
    public float maxTimeToSpawn;
    float t = 0f;

    Renderer r;
    Color color;

    Transform player;
    Vector3 initialPosition;
    bool trackPosition = true;
    float distanceMoved = 0f;
    float vanishDistance = 5f;
    
    void Start()
    {
        timeBetweenAppearances = Random.Range(minTimeToSpawn, maxTimeToSpawn);

        r = GetComponent<Renderer>();
        r.enabled = false;

        player = GameObject.Find("XR Origin (XR Rig)").transform;
    }

    void Update()
    {
        if (t > timeBetweenAppearances)
        {
            //place at certain dist and angle from player
            spawnAngle = Random.Range(0f, 2 * Mathf.PI);
            transform.position = player.position + spawnDistance * new Vector3(Mathf.Cos(spawnAngle), 0f, Mathf.Sin(spawnAngle));
            transform.LookAt(player.position);

            r.enabled = true;

            initialPosition = player.position;
            trackPosition = true;
            timeBetweenAppearances = Mathf.Infinity;
            t = 0f;
        }

        if (trackPosition)
        {
            //reduce opacity based on player movement
            distanceMoved += Vector3.Distance(initialPosition, player.position);
            initialPosition = player.position;

            color = r.material.color;
            if (distanceMoved > 0f)
                color.a = (distanceMoved - vanishDistance) / -vanishDistance;
            else
                color.a = 1f;
            r.material.color = color;

            if (color.a < 0.01f)
            {
                r.enabled = false;
                trackPosition = false;
                distanceMoved = 0f;
                timeBetweenAppearances = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            }
        }
        else
        {
            t += Time.deltaTime;
        }
    }
}
