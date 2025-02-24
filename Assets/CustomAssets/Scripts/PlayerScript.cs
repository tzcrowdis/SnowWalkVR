using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private Transform XRPlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Keep the player at the position of the XR origin
        transform.position = XRPlayerTransform.position;
    }
}
