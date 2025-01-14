using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public float gameTime;

    public float actOneStartTime;
    public float actTwoStartTime;
    public float actThreeStartTime;

    public GameObject player;

    public GameObject strangeTree;
    public GameObject bonfire;
    public GameObject wendigo;

    int act;

    void Start()
    {
        gameTime = Time.realtimeSinceStartup;
        act = 0;
    }

    void Update()
    {
        if (act != 3)
            UpdateAct();
    }

    void UpdateAct()
    {
        // TODO complete update act function
        if (act == 0 && actOneStartTime < gameTime)
        {
            // spawn strange tree
            Instantiate(strangeTree);

            // enable crow spawner (spawns crows randomly around player) (copmponent on player?)
            //player.AddComponent<CrowSpawner>();

            // enable specific music too???

            act = 1;
        }
        else if (act == 1 && actTwoStartTime < gameTime)
        {
            // delete strange tree
            Destroy(strangeTree);

            // delete crow spawner
            //Destroy(player.GetComponent<CrowSpawner>());

            // spawn in bonfire
            Instantiate(bonfire); // TODO set position

            act = 2;
        }
        else if (act == 2 && actThreeStartTime < gameTime)
        {
            // delete bonfire
            Destroy(bonfire);

            // spawn in wendigo
            Instantiate(wendigo);

            act = 3;
        }
    }
}
