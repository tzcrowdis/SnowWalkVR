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
    public float smoothFactor = 10f;

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


    void FixedUpdate()
    {
        if (!isSnapTurning)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, target.position, smoothFactor * Time.fixedDeltaTime));

            rb.MoveRotation(Quaternion.Slerp(transform.rotation, target.rotation, smoothFactor * Time.fixedDeltaTime));
        }
    }

    void LateUpdate()
    {
        // Override hand position and rotation while executing a snap turn
        if (isSnapTurning)
        {
            rb.isKinematic = true; // Disable physics
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        else
        {
            rb.isKinematic = false;
        }

    }
}
