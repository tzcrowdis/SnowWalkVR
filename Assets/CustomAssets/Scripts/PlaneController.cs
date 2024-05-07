using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    GameObject[] trees = new GameObject[5];

    float planeLength = 100f;

    public Transform player;
    
    void Awake()
    {
        for (int i = 0; i < trees.Length; i++)
            trees[i] = Resources.Load($"Trees/CreepyTree{i + 1}") as GameObject;

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
            x = Random.Range(-planeLength / 2, planeLength / 2);
            z = Random.Range(-planeLength / 2, planeLength / 2);

            //check x, z conflicts
            while (Vector3.Distance(new Vector3(x, 0, z), player.transform.position) < 10f)
            {
                x = Random.Range(-planeLength / 2, planeLength / 2);
                z = Random.Range(-planeLength / 2, planeLength / 2);
            }

            //instantiate the tree with some randomness
            treeNumber = Random.Range(0, trees.Length);
            tree = Instantiate(trees[treeNumber], transform);
            tree.transform.localPosition = new Vector3(x, 0, z);
            tree.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
            tree.transform.localScale += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }
    }
}
