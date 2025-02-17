using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WorldController : MonoBehaviour
{
    public ForestController forestController;
    public CrowSpawner crowSpawner;
    public ProgressTracker progressTracker;
    public CharacterControllerDriver player;
    public static WorldController Instance { get; private set; }

    private void Awake() 
{ 
    // Delete duplicates
    if (Instance != null && Instance != this) 
    { 
        Destroy(this); 
    } 
    else 
    { 
        Instance = this; 
    } 
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
