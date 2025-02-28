using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoScreams : MonoBehaviour
{
    [Header("Screaming")]
    public AudioSource vocal;
    public AudioClip[] closeScreams;
    public AudioClip[] cicking;
    public AudioClip[] distantScreams;
    public AudioClip awakeScream;

    void Start()
    {
        vocal.minDistance = 15;
        vocal.PlayOneShot(awakeScream);
        vocal.minDistance = 5;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !vocal.isPlaying)
            vocal.PlayOneShot(closeScreams[Random.Range(0, closeScreams.Length)]);
    }
}
