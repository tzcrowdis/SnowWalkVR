using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    GameObject[] trees = new GameObject[5];

    float planeLength;

    public Transform player;
    
    void Awake()
    {
        for (int i = 0; i < trees.Length; i++)
            trees[i] = Resources.Load($"Trees/CreepyTree{i + 1}") as GameObject;

        planeLength = 100 * transform.localScale.x;

        player = GameObject.Find("XR Origin (XR Rig)").transform;
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
        }
    }

    void UniformForest()
    {
        float density = 400f;

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
            tree.transform.localScale += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }
    }
}
