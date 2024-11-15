using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Crow : MonoBehaviour
{
    float startAltitude;
    public float startleDistance;

    Transform player;

    public float flightAltitude;
    public float flightSpeed;

    public float takeoffSpeed = 0.1f;
    float takeoffTime = 0f;
    public float takeoffAngle;
    Quaternion takeoffStartRotation;
    Quaternion takeoffEndRotation;
    
    enum CrowState
    {
        Idle,
        TakeOff,
        SoloFlight,
        MurderFlight
    }
    CrowState state;

    
    void Start()
    {
        player = GameObject.Find("XR Origin (XR Rig)").transform;

        startAltitude = transform.position.y;
        
        state = StartState();

        takeoffStartRotation = Quaternion.Euler(new Vector3(takeoffAngle, transform.eulerAngles.y, transform.eulerAngles.z));
        takeoffEndRotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z));
    }

    void Update()
    {
        // TODO integrate animation control in each function on state change
        
        switch (state)
        {
            case CrowState.Idle:
                Idle();
                break;
            case CrowState.TakeOff:
                TakeOff();
                break;
            case CrowState.SoloFlight:
                SoloFlight();
                break;
        }
    }

    // crow waits until startled
    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < startleDistance)
            state = CrowState.TakeOff;
    }

    // crow flys forward and rises to altitude by rotating
    void TakeOff()
    {
        transform.position += flightSpeed * transform.forward * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(takeoffStartRotation, takeoffEndRotation, takeoffTime);
        takeoffTime += takeoffSpeed * Time.deltaTime;

        if (takeoffTime > 1)
        {
            // TODO: check if solo or in a murder

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            state = CrowState.SoloFlight;
            takeoffTime = 0f;
        }
    }

    // a single crow flying at the desired altitude
    void SoloFlight()
    {
        transform.position += flightSpeed * transform.forward * Time.deltaTime;

        if (transform.position.y < flightAltitude)
        {
            state = CrowState.TakeOff;
        }
    }

    // a murder of crows all flying together at altitude
    void MurderFlight()
    {
        
    }

    // decide what state crow was instantiated in
    CrowState StartState()
    {
        if (startAltitude < flightAltitude)
        {
            if (Vector3.Distance(transform.position, player.position) < startleDistance)
                return CrowState.TakeOff;
            else
                return CrowState.Idle;
        }
        else
            return CrowState.SoloFlight;
    }
}
