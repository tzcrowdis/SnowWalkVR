using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTriggers : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("XR Origin"))
        {
            ForestController forestController = WorldController.Instance.forestController;
            forestController.currentForest = gameObject.transform;
            forestController.changeEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("XR Origin"))
        {
            ForestController forestController = WorldController.Instance.forestController;
            forestController.oldForest = gameObject.transform;
            forestController.changeExit = true;
        }
    }
}
