using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTriggers : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        other.GetComponent<ForestController>().currentForest = gameObject.transform;
        other.GetComponent<ForestController>().changeEnter = true;
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<ForestController>().oldForest = gameObject.transform;
        other.GetComponent<ForestController>().changeExit = true;
    }
}
