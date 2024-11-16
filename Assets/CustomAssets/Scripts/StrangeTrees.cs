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
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        ogVertices = mesh.vertices;
    }

    void Update()
    {
        Wiggle();
    }

    void Wiggle()
    {
        // formula for ripple:
        // x(t) = Amplitude * sin(2(pi) * frequency * (distance_from_source - t * propogation_speed))
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3
            (
                // assumes wave starts at 0 and propogates upward
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
