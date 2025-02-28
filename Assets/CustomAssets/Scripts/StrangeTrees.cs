using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeTrees : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] ogVertices;

    [Header("Wiggle Properties")]
    public float amplitude = 1;
    public float frequency = 1;
    public float propogationSpeed = 1;
    float time = 0;
    public float timeToActNormal;

    bool wiggle = true;
    
    [Header("Following")]
    public float spawnDistance;
    float spawnAngle;
    Transform player;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        ogVertices = mesh.vertices;

        player = GameObject.Find("XR Origin (XR Rig)").transform;
    }

    void Update()
    {
        // tree follows player
        if (Vector3.Distance(player.position, transform.position) > spawnDistance)
            MovePosition();

        if (wiggle)
            Wiggle();
    }

    void MovePosition()
    {
        spawnAngle = Random.Range(0f, 2 * Mathf.PI); // TODO: set range to behind the player?
        transform.position = player.position + spawnDistance * new Vector3(Mathf.Cos(spawnAngle), 0f, Mathf.Sin(spawnAngle));
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
        while (t < timeToActNormal)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // snap back from wiggle position
        wiggle = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            MovePosition();
    }
}
