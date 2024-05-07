using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    List<AudioClip> walking;

    AudioSource source;
    
    void Start()
    {
        for (int i = 1; i <= 12; i++)
        {
            if (i < 10)
                walking.Add(Resources.Load($"SnowWalking/Footsteps_Snow_Walk_0{i}") as AudioClip);
            else
                walking.Add(Resources.Load($"SnowWalking/Footsteps_Snow_Walk_{i}") as AudioClip);
        }
        
        source = GetComponent<AudioSource>();

        Debug.Log(walking.Count);
    }

    // Update is called once per frame
    void Update()
    {
        //if moving
        //

        if (Input.GetKeyDown(KeyCode.F))
        {
            int r = Random.Range(0, walking.Count);
            source.PlayOneShot(walking[r]);
        }
            
    }
}
