using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class PlaneController : MonoBehaviour
{
    GameObject[] trees = new GameObject[5]; // stores the types of trees

    float planeLength;
    public float length;

    public Transform player;

    public NavMeshSurface surface;
    NavMeshLink link;
    
    void Awake()
    {
        for (int i = 0; i < trees.Length; i++)
            trees[i] = Resources.Load($"Trees/CreepyTree{i + 1}") as GameObject;

        player = GameObject.Find("XR Origin (XR Rig)").transform;

        length = player.gameObject.GetComponent<ForestController>().d;
        planeLength = 2 * length * transform.localScale.x; // 2x bc model scale is all 0.5
    }

    void Update()
    {
        
    }

    public void PopulatePlane(string forestType)
    {
        switch (forestType)
        {
            case "UniformForest":
                UniformForest();
                break;
            case "Clear":
                ClearPlane();
                break;
        }

        BuildForestNavMesh();
    }

    void BuildForestNavMesh()
    {
        // surface on individual planes
        surface = gameObject.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Children;
        surface.BuildNavMesh();

        float agentRadius = 1f; // agent types radius (plus a little)

        // link between planes
        link = gameObject.AddComponent<NavMeshLink>();
        link.width = length;
        link.costModifier = 0;
        link.startPoint = new Vector3(length / 2 - agentRadius, 0, 0);
        link.endPoint = new Vector3(length / 2 + agentRadius, 0, 0);

        link = gameObject.AddComponent<NavMeshLink>();
        link.width = length;
        link.costModifier = 0;
        link.startPoint = new Vector3(-length / 2 - agentRadius, 0, 0);
        link.endPoint = new Vector3(-length / 2 + agentRadius, 0, 0);

        link = gameObject.AddComponent<NavMeshLink>();
        link.width = length;
        link.costModifier = 0;
        link.startPoint = new Vector3(0, 0, length / 2 - agentRadius);
        link.endPoint = new Vector3(0, 0, length / 2 + agentRadius);

        link = gameObject.AddComponent<NavMeshLink>();
        link.width = length;
        link.costModifier = 0;
        link.startPoint = new Vector3(0, 0, -length / 2 - agentRadius);
        link.endPoint = new Vector3(0, 0, -length / 2 + agentRadius);
    }

    void UniformForest()
    {
        float density = player.gameObject.GetComponent<ForestController>().uniformForestDensity;

        float x;
        float z;
        int treeNumber;
        GameObject tree;

        for (int i = 0; i < density; i++)
        {
            x = Random.Range(-planeLength, planeLength);
            z = Random.Range(-planeLength, planeLength);

            //check x, z conflicts
            while (Vector3.Distance(transform.position + new Vector3(x, 0, z), player.transform.position) < 15f)
            {
                x = Random.Range(-planeLength, planeLength);
                z = Random.Range(-planeLength, planeLength);
            }

            //instantiate the tree with some randomness
            treeNumber = Random.Range(0, trees.Length);
            tree = Instantiate(trees[treeNumber], transform);
            tree.transform.localPosition = new Vector3(x, 0, z);
            tree.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
            tree.transform.localScale *= 1 / transform.localScale.x;
            tree.transform.localScale += new Vector3(Random.Range(-0.1f, 0.5f), Random.Range(-0.1f, 1f), Random.Range(-0.1f, 0.5f));
        }
    }

    void ClearPlane()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
