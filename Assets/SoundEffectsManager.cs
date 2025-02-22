using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFxClip(AudioClip audioClip, Transform spawnTransform)
    {

    }
}
