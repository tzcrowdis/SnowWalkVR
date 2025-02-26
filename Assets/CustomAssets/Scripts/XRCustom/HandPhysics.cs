using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPhysics : MonoBehaviour
{

    public Transform XRControllerTransform;
    private Rigidbody rb;
    private Collider[] handColliders;
    private bool isSnapTurning = false;
    //public InputActionReference snapTurnAction;

    //The controller interactor
    private XRDirectInteractor interactor;
    public CharacterController characterController;

    private Vector3 lastPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {

        //Get rigidbody
        rb = GetComponent<Rigidbody>();

        //hands got move fuckin fast
        rb.maxAngularVelocity = Mathf.Infinity;
        rb.maxLinearVelocity = Mathf.Infinity;

        //Get all hand colliders
        handColliders = GetComponentsInChildren<Collider>();

        interactor = XRControllerTransform.GetComponentInChildren<XRDirectInteractor>();

        /*
        //Subscribe to snap turn events
        snapTurnAction.action.Enable();
        snapTurnAction.action.performed += OnSnapTurnPerformed;
        snapTurnAction.action.canceled += OnSnapTurnCanceled;
        */
    }


    
    /*

    //In the awake method, connect the hand to the body.
    private void Awake()
    {
        handToBodyJoint = playerRB.gameObject.AddComponent<ConfigurableJoint>();
        //joint setup, choosing the body and zeroing out anchors
        handToBodyJoint.connectedBody = rb;
        handToBodyJoint.autoConfigureConnectedAnchor = false;
        handToBodyJoint.anchor = Vector3.zero;
        handToBodyJoint.connectedAnchor = Vector3.zero;

        //setup the joint drive by making a new one, should probably make these values editable.
        handToBodyJoint.xDrive = new JointDrive
        {
            positionSpring = 1000f,
            positionDamper = 100f,
            maximumForce = 750f
        };
        //set all the other drives used to this drive.
        handToBodyJoint.yDrive = handToBodyJoint.xDrive;
        handToBodyJoint.zDrive = handToBodyJoint.xDrive;
        //we use the slerp drive instead to make rotations slightly easier
        handToBodyJoint.rotationDriveMode = RotationDriveMode.Slerp;
        handToBodyJoint.slerpDrive = handToBodyJoint.xDrive;
    }
    */

    public void EnableHandCollider()
    {
        if (!interactor.isSelectActive)
        {
            foreach (var item in handColliders) 
            {
                item.enabled = true;
            }

        }

    }

    public void DisableHandCollider()
    {
        if (interactor.isSelectActive)
        {
            foreach (var item in handColliders) 
            {
                item.enabled = false;
            }
        }
    }

/*
    private void OnSnapTurnPerformed(InputAction.CallbackContext context)
    {
        isSnapTurning = true;
    }

    private void OnSnapTurnCanceled(InputAction.CallbackContext context)
    {
        isSnapTurning = false;
    }
    */

    void FixedUpdate()
    {

            rb.isKinematic = false;

            // Calculate the desired velocity to reach the XRControllerTransform position
            Vector3 direction = XRControllerTransform.position - transform.position;
            rb.velocity = direction / Time.fixedDeltaTime;


            // Calculate the desired angular velocity to reach the XRControllerTransform rotation
            Quaternion rotationDelta = XRControllerTransform.rotation * Quaternion.Inverse(transform.rotation);
            rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);

            // Normalize the angle to the range [-180, 180] to avoid flips
            if (angle > 180f)
            {
                angle -= 360f;
            }

            rb.angularVelocity = (angle * axis) * Mathf.Deg2Rad / Time.fixedDeltaTime;

    } 
}