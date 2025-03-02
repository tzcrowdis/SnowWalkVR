using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnowPileScript : MonoBehaviour
{
    public GameObject snowballPrefab;

    public XRInteractionManager interactionManager;

    void Awake()
    {     
        if (interactionManager == null)
        {
            interactionManager = FindAnyObjectByType<XRInteractionManager>();
        }
    }

    void Update()
    {
        // destroy snow pile if empty
        if (transform.childCount == 0)
            Destroy(gameObject);
    }

    public void setSnowballMaterial(Material material)
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = material;
        }
    }

    public void grabSnowball(SelectEnterEventArgs args)
    {
        //spawn Snowball
        GameObject snowball = Instantiate(snowballPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        //attach to hand
        interactionManager.SelectEnter(args.interactorObject, snowball.GetComponent<XRGrabInteractable>());
        snowball.GetComponent<SnowBall>().setSnowballHover(false);

        //Cancel hand selection
        interactionManager.SelectExit(args.interactorObject, this.GetComponent<XRSimpleInteractable>());

        // destroy next snowball
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }
}
