using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnowBall : MonoBehaviour
{
    public Material snowBallMat;
    public Material snowBallSelectedMat;
    private void Start()
    {
        // TODO: pick up sound effect
    }

    public void setSnowballHover(bool setValue)
    {
        if (setValue && !GetComponent<XRGrabInteractable>().isSelected)
        {
            GetComponent<MeshRenderer>().material = snowBallSelectedMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = snowBallMat;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy(gameObject);

        // TODO: break sound effect

        // TODO: spawn snow particles at point of collision

        // TODO: decal on object collided with
    }
}
