using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnowPileScript : MonoBehaviour
{
    public GameObject snowballPrefab;

    public XRInteractionManager interactionManager;

    // Start is called before the first frame update
    void Start()
    {     
        if (interactionManager == null)
        {
            interactionManager = FindAnyObjectByType<XRInteractionManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void grabSnowball(SelectEnterEventArgs args)
    {
        //spawn Snowball
        GameObject snowball = Instantiate(snowballPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //attach to hand
        interactionManager.SelectEnter(args.interactorObject, snowball.GetComponent<XRGrabInteractable>());

    }
}
