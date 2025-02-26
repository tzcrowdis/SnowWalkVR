using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Transform XRPlayerTransform;
    private Vector3 locomotionTransform;
    private CharacterController characterController;
    private Rigidbody[] hands;

    //private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        hands = GetComponentsInChildren<Rigidbody>();
        characterController =  GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log(XRPlayerTransform.position - locomotionTransform);
    
        
        // Update position changes to the hands
        foreach (Rigidbody hand in hands) 
        {
            hand.velocity += characterController.velocity;
        }
        */

        transform.position = XRPlayerTransform.position;
        transform.rotation = XRPlayerTransform.rotation;
    }
}
