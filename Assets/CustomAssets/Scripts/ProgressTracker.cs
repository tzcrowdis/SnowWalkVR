using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    float gameTime;

    public float actOneStartTime;
    public float actTwoStartTime;
    public float actThreeStartTime;

    public GameObject player;

    public GameObject strangeTreePrefab;
    GameObject strangeTree;
    public GameObject bonfirePrefab;
    GameObject bonfire;
    public GameObject wendigoPrefab;
    GameObject wendigo;

    int act;

    void Start()
    {
        player.GetComponent<CrowSpawner>().enabled = false;
        act = 0;
    }

    void Update()
    {
        gameTime = Time.realtimeSinceStartup;

        if (act != 3)
            UpdateAct();
    }

    void UpdateAct()
    {
        Debug.Log("Game Time: " + gameTime);
        
        // TODO test
        // TODO add act specific ambient music???
        if (act == 0 && actOneStartTime < gameTime)
        {
            // spawn strange tree
            strangeTree = Instantiate(strangeTreePrefab);

            // enable crow spawner
            player.GetComponent<CrowSpawner>().enabled = true;

            act = 1;
        }
        else if (act == 1 && actTwoStartTime < gameTime)
        {
            // delete strange tree
            Destroy(strangeTree);

            // delete crow spawner and crow
            player.GetComponent<CrowSpawner>().enabled = false;
            foreach (GameObject crow in GameObject.FindGameObjectsWithTag("Crow"))
                Destroy(crow);

            // spawn in bonfire
            bonfire = Instantiate(bonfirePrefab); // TODO set position

            act = 2;
        }
        else if (act == 2 && actThreeStartTime < gameTime)
        {
            // delete bonfire and ghosts
            Destroy(bonfire);
            foreach (GameObject ghost in GameObject.FindGameObjectsWithTag("Ghost"))
                Destroy(ghost);

            // spawn in wendigo
            wendigo = Instantiate(wendigoPrefab);

            act = 3;
        }
    }
}
