using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBooth : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            Destroy(gameObject);
    }
}
