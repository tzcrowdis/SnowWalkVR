using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandPhysics : MonoBehaviour
{

    public Transform target;
    private Rigidbody rb;
    private Collider[] handColliders;
    private bool isSnapTurning = false;
    public InputActionReference snapTurnAction;
    
    private float smoothFactor = 50f;




    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody
        rb = GetComponent<Rigidbody>();

        //hands got move fuckin fast
        rb.maxAngularVelocity = Mathf.Infinity;

        //Get all hand colliders
        handColliders = GetComponentsInChildren<Collider>();

        //Subscribe to snap turn events
        snapTurnAction.action.Enable();
        snapTurnAction.action.performed += OnSnapTurnPerformed;
        snapTurnAction.action.canceled += OnSnapTurnCanceled;
    }

    public void EnableHandCollider()
    {
        foreach (var item in handColliders) 
        {
            item.enabled = true;
        }
    }

    public void DisableHandCollider()
    {
        foreach (var item in handColliders) 
        {
            item.enabled = false;
        }
    }

    private void OnSnapTurnPerformed(InputAction.CallbackContext context)
    {
        isSnapTurning = true;
    }

    private void OnSnapTurnCanceled(InputAction.CallbackContext context)
    {
        isSnapTurning = false;
    }

    void Update()
    {

        
    }

    void FixedUpdate()
    {
        // Calculate the desired velocity to reach the target position
        Vector3 direction = (target.position - rb.position).normalized;
        float distance = Vector3.Distance(rb.position, target.position);
        rb.velocity = direction * (distance * smoothFactor);

        // Calculate the desired angular velocity to reach the target rotation
        Quaternion targetRotation = target.rotation;
        Quaternion rotationDelta = targetRotation * Quaternion.Inverse(rb.rotation);
        rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);

        // Normalize the angle to the range [-180, 180] to avoid flips
        if (angle > 180f)
        {
            angle -= 360f;
        }

        rb.angularVelocity = axis * (angle * Mathf.Deg2Rad * smoothFactor);
    }

}
