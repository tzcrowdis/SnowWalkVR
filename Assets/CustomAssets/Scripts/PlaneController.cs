using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using UnityEngine.XR.Interaction.Toolkit;


public class PlaneController : MonoBehaviour
{
    GameObject[] trees = new GameObject[5]; // stores the types of trees

    float planeLength;
    public float length;

    public Transform player;

    private ForestController forestController;

    [Header("SnowPiles")]
    public GameObject snowpilePrefab;
    public int snowpileDensity;

    void Awake()
    {
        for (int i = 0; i < trees.Length; i++)
            trees[i] = Resources.Load($"Trees/CreepyTree{i + 1}") as GameObject;

        player = WorldController.Instance.player.transform;

        forestController = WorldController.Instance.forestController;

        length = forestController.d;
        planeLength = 2 * length * transform.localScale.x; // 2x bc model scale is all 0.5
    }

    public void PopulatePlane(ForestController.ForestType forest)
    {
        switch (forest)
        {
            case ForestController.ForestType.Uniform:
                UniformForest();
                break;
            case ForestController.ForestType.Clear:
                ClearPlane();
                break;
        }

        SnowPiles(); // every plane should have a uniform distribution of snow piles
    }

    void UniformForest()
    {
        ClearPlane();
        
        float density = forestController.uniformForestDensity;

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

    void SnowPiles()
    {
        float x;
        float y = 0.75f;
        float z;
        GameObject snowpile;

        for (int i = 0; i < snowpileDensity; i++)
        {
            // initialize snowpile
            snowpile = Instantiate(snowpilePrefab);
            x = Random.Range(-planeLength, planeLength);
            z = Random.Range(-planeLength, planeLength);
            snowpile.transform.localPosition = new Vector3(x, y, z);

            // check for position conflicts
            bool conflict = true;
            while (conflict)
            {
                bool conflictFound = false;
                foreach (Transform child in transform)
                {
                    if (Vector3.Distance(snowpile.transform.position, child.position) < 1f)
                    {
                        x = Random.Range(-planeLength, planeLength);
                        z = Random.Range(-planeLength, planeLength);
                        conflictFound = true;
                        break;
                    }
                }

                if (!conflictFound)
                    conflict = false;
            }

            // set final position and rotation
            snowpile.transform.parent = transform;
            snowpile.transform.localPosition = new Vector3(x, y, z);
            snowpile.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
        }
    }

    void ClearPlane()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
