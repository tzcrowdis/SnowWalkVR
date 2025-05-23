using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ProgressTracker : MonoBehaviour
{
    public float gameTime;

    [Header("Act Start Times [minutes]")]
    [Tooltip("start time in minutes (not relative to previous act)")]
    public float actOneStartTime;
    [Tooltip("start time in minutes (not relative to previous act)")]
    public float actTwoStartTime;
    [Tooltip("start time in minutes (not relative to previous act)")]
    public float actThreeStartTime;

    [Header("Objects Relevant to Progression")]
    public GameObject player;

    public GameObject strangeTreePrefab;
    GameObject strangeTree;
    public GameObject bonfirePrefab;
    GameObject bonfire;
    public GameObject wendigoPrefab;
    GameObject wendigo;

    public NavMeshSurface ghostNavMesh;
    public NavMeshSurface wendigoNavMesh;

    int act;

    void Start()
    {
        actOneStartTime *= 60;
        actTwoStartTime *= 60;
        actThreeStartTime *= 60;
        
        GetComponent<CrowSpawner>().enabled = false;
        act = 0;

        ghostNavMesh.gameObject.SetActive(false);
        wendigoNavMesh.gameObject.SetActive(false);

        gameTime = 0;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (act != 3)
            UpdateAct();
    }

    void UpdateAct()
    {
        // TODO add act specific ambient music???
        if (act == 0 && actOneStartTime < gameTime)
        {
            // spawn strange tree
            strangeTree = Instantiate(strangeTreePrefab);

            // enable crow spawner
            GetComponent<CrowSpawner>().enabled = true;

            act = 1;
        }
        else if (act == 1 && actTwoStartTime < gameTime)
        {
            // delete strange tree
            Destroy(strangeTree);

            // delete crow spawner and crow
            GetComponent<CrowSpawner>().enabled = false;
            foreach (GameObject crow in GameObject.FindGameObjectsWithTag("Crow"))
                Destroy(crow);
            
            // ghost nav surface
            player.GetComponent<NavMeshObstacle>().enabled = false;
            ghostNavMesh.gameObject.SetActive(true);
            
            // spawn in bonfire
            bonfire = Instantiate(bonfirePrefab, BonfireSpawnPosition(), Quaternion.identity);
            
            act = 2;
        }
        else if (act == 2 && actThreeStartTime < gameTime)
        {
            // delete bonfire and ghosts
            Destroy(bonfire);
            foreach (GameObject ghost in GameObject.FindGameObjectsWithTag("Ghost"))
                Destroy(ghost);

            // toggle ghost and wendigo nav meshes
            ghostNavMesh.gameObject.SetActive(false);
            wendigoNavMesh.gameObject.SetActive(true);

            player.GetComponent<NavMeshObstacle>().enabled = true;

            // spawn in wendigo (at burnt bonfire position and facing the player)
            wendigo = Instantiate(wendigoPrefab, WendigoSpawnLocation(), Quaternion.identity);

            act = 3;
        }
    }

    Vector3 BonfireSpawnPosition()
    {
        float spawnDistance = 40f;
        Vector3 spawnPosition = player.transform.position + player.transform.forward * spawnDistance;
        spawnPosition.y = 0.2f;
        return spawnPosition;
    }

    Vector3 WendigoSpawnLocation()
    {
        float spawnDistance = 55f; // set based on circling radius
        Vector3 spawnPosition = player.transform.position - player.transform.forward * spawnDistance;
        return spawnPosition;
    }
}
