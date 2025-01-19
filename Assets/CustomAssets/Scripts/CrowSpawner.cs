using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSpawner : MonoBehaviour
{
    public GameObject crow;

    void Start()
    {
        
    }

    void Update()
    {
        // TODO consider adding multiple crows (count grows as act proceeds?)
        
        if (!GameObject.Find("FlyingCrow(Clone)"))
        {
            Instantiate(crow);
        }
    }
}
