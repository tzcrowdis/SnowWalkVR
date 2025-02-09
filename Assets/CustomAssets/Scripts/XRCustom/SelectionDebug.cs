using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabDebug : MonoBehaviour
{
    void Start()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(interactor => Debug.Log("Grabbed!"));
    }
}