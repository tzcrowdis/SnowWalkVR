using UnityEngine;
using UnityEngine.InputSystem;

public class GripInputDebug : MonoBehaviour
{
    public InputActionReference gripActionReference;

    private void OnEnable()
    {
        // Enable the action and subscribe to the performed event
        gripActionReference.action.Enable();
        gripActionReference.action.performed += OnGripPerformed;
        gripActionReference.action.canceled += OnGripCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe from the events
        gripActionReference.action.performed -= OnGripPerformed;
        gripActionReference.action.canceled -= OnGripCanceled;
    }

    private void OnGripPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Grip Pressed");
    }

    private void OnGripCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Grip Released");
    }
}