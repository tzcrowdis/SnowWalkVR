using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeTrees : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] ogVertices;

    public float amplitude = 1;
    public float frequency = 1;
    public float propogationSpeed = 1;
    float time = 0;

    bool wiggle = true;
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        ogVertices = mesh.vertices;
    }

    void Update()
    {
        if (wiggle)
            Wiggle();
    }

    void OnBecameInvisible()
    {
        wiggle = true;
    }

    void OnBecameVisible()
    {
        StartCoroutine(ActNormal());
    }

    
    public IEnumerator ActNormal()
    {
        // delay
        float t = 0;
        float delay = 0.5f;
        while (t < delay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // snap back from wiggle position
        wiggle = false;

        // TODO: try lerping the amplitude

        // NOTE: this lags the game HAAAARD (need to optimize mesh vertices???)
        /*
        t = 0;
        float speed = 1;
        
        while (t < 1)
        {
            for (var i = 0; i < vertices.Length; i++)
                vertices[i] = Vector3.Lerp(mesh.vertices[i], ogVertices[i], speed * t);
            
            t += Time.deltaTime;
            yield return null;
        }
        */

        mesh.vertices = ogVertices;
        mesh.RecalculateBounds();
        yield return null;
    }

    void Wiggle()
    {
        // formula for ripple:
        // x(t) = Amplitude * sin(2(pi) * frequency * (distance_from_source - t * propogation_speed))
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3
            (
                // assumes wave starts at base and propogates upward
                amplitude * Mathf.Sin(2 * Mathf.PI * frequency * (vertices[i].y - time * propogationSpeed)) + ogVertices[i].x,
                vertices[i].y,
                vertices[i].z
            );
        }
        time += Time.deltaTime;

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
