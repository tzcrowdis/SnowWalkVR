using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// source: https://discussions.unity.com/t/how-to-stop-hands-going-though-objects/923008

public class HandFollowController : MonoBehaviour
{
    Rigidbody rb;
    Transform target;

    private void Start()
    {
        if (gameObject.name.Contains("Right"))
            target = GameObject.Find("Right Hand Position").transform;
        else if (gameObject.name.Contains("Left"))
            target = GameObject.Find("Left Hand Position").transform;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // track position
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        // track rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
}