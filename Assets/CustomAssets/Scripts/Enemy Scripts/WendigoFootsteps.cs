using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoFootsteps : MonoBehaviour
{
    [Header("Walking")]
    List<AudioClip> walking = new List<AudioClip>();
    AudioSource walkingSource;

    void Start()
    {
        walkingSource = GetComponent<AudioSource>();
        
        // load walking audio clips
        for (int i = 1; i <= 12; i++)
        {
            if (i < 10)
                walking.Add(Resources.Load($"SnowWalking/Footsteps_Snow_Walk_0{i}") as AudioClip);
            else
                walking.Add(Resources.Load($"SnowWalking/Footsteps_Snow_Walk_{i}") as AudioClip);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!walkingSource.isPlaying)
            walkingSource.PlayOneShot(walking[Random.Range(0, walking.Count)]);
    }
}
