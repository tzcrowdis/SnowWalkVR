using UnityEngine;
using UnityEngine.InputSystem;

public class DebugHand : MonoBehaviour
{
    public Transform XRControllerTransform;
    public Transform XRLocomotionTransform;



    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.SetPositionAndRotation(XRControllerTransform.position, XRControllerTransform.rotation);
    }

}