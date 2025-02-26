using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSounds : MonoBehaviour
{
    List<AudioClip> walking = new List<AudioClip>();
    AudioSource[] sources;

    public float timeBetweenSteps;
    int stepChoice;

    public float distBtwnSteps;
    Vector3 previousPosition;

    void Start()
    {
        for (int i = 1; i <= 12; i++)
        {
            if (i < 10)
                walking.Add(Resources.Load($"SnowWalking/Footsteps_Snow_Walk_0{i}") as AudioClip);
            else
                walking.Add(Resources.Load($"SnowWalking/Footsteps_Snow_Walk_{i}") as AudioClip);
        }

        sources = GetComponents<AudioSource>();

        sources[1].Play();
    }

    // TODO rework for 4 legs

    void Update()
    {
        //footstep sounds based on distance
        if (Vector3.Distance(previousPosition, transform.position) > distBtwnSteps)
        {
            stepChoice = Random.Range(0, walking.Count);
            sources[0].PlayOneShot(walking[stepChoice]);

            previousPosition = transform.position;
        }
    }
}
