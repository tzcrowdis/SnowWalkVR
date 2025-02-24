using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Crow : MonoBehaviour
{
    float startAltitude;
    public float startleDistance;

    float destroyDistance;
    float spawnDistance;
    public float spawnHeight;

    Transform player;

    public float flightAltitude;
    public float flightSpeed;

    public float takeoffSpeed = 0.1f;
    float takeoffTime = 0f;
    public float takeoffAngle;
    Quaternion takeoffStartRotation;
    Quaternion takeoffEndRotation;

    Animator animator;

    AudioSource crowStartled;

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
        destroyDistance = 100f;
        spawnDistance = 2 * startleDistance;

        player = GameObject.Find("XR Origin (XR Rig)").transform;

        float spawnAngle = Random.Range(0f, 2 * Mathf.PI);
        transform.position = player.position + spawnDistance * new Vector3(Mathf.Cos(spawnAngle), 0f, Mathf.Sin(spawnAngle));
        transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        transform.Rotate(0, Random.Range(0f, 360f), 0);

        startAltitude = transform.position.y;
        
        state = StartState();

        animator = GetComponent<Animator>();

        crowStartled = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {   
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
        {
            // look away from player
            Vector3 forward = (transform.position - player.position).normalized;
            forward.y = 0f;
            transform.rotation = Quaternion.LookRotation(forward);

            takeoffStartRotation = Quaternion.Euler(new Vector3(takeoffAngle, transform.eulerAngles.y, transform.eulerAngles.z));
            takeoffEndRotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z));

            animator.SetBool("startled", true);

            state = CrowState.TakeOff;

            crowStartled.pitch += Random.Range(-0.5f, 0.5f);
            crowStartled.Play();
        }
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

        /*if (transform.position.y < flightAltitude)
        {
            state = CrowState.TakeOff;
        }*/

        if (Vector3.Distance(transform.position, player.position) > destroyDistance)
        {
            Destroy(gameObject);
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
