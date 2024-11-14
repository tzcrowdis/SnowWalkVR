using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    float flyingAltitude = 10f;
    
    string state = "";
    
    void Start()
    {
        if (transform.position.y < flyingAltitude)
        {
            state = "TakeOff";
        }
        else
        {
            state = "SoloFlight";
        }
    }

    void Update()
    {
        switch (state)
        {
            case "TakeOff":
                TakeOff();
                break;
            case "SoloFlight":
                SoloFlight();
                break;
        }
    }

    // crow flys forward and rises to altitude
    void TakeOff()
    {
        
    }

    // a single crow flying at the desired altitude
    void SoloFlight()
    {

    }

    // a murder of crows all flying together at altitude
    void MurderFlight()
    {
        
    }
}
