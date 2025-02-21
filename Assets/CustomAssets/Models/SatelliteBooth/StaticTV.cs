using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTV : MonoBehaviour
{
    // source: https://gamedev.stackexchange.com/questions/35030/can-i-use-an-animated-gif-as-a-texture

    [SerializeField] private Texture2D[] frames;
    [SerializeField] private float fps = 10.0f;

    public Material mat;

    void Start()
    {
        //mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        int index = (int)(Time.time * fps);
        index = index % frames.Length;
        mat.mainTexture = frames[index];
    }
}
