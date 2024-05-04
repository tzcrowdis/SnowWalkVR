using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTriggers : MonoBehaviour
{
    void OnTriggerEnter(Collider player)
    {
        Debug.Log(player.gameObject.name);

        player.GetComponent<ForestController>().newGrid = gameObject.transform;
        player.GetComponent<ForestController>().changeEnter = true;
    }

    void OnTriggerExit(Collider player)
    {
        player.GetComponent<ForestController>().oldGrid = gameObject.transform;
        player.GetComponent<ForestController>().changeExit = true;
    }
}
