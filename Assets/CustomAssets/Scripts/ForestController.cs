using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class ForestController : MonoBehaviour
{
    [HideInInspector]
    public bool changeEnter, changeExit;

    [HideInInspector]
    public List<GameObject> grid;

    [HideInInspector]
    public Transform oldForest, currentForest;

    public float d = 50f; //side length of snow plane
    public float uniformForestDensity;

    public NavMeshSurface navmesh;

    GameObject snowPlaneResource;

    public enum ForestType
    {
        Uniform,
        Clear
    }
    ForestType forest = ForestType.Uniform; // NOTE: all uniform for now

    void Start()
    {
        changeEnter = false;
        changeExit = false;

        snowPlaneResource = Resources.Load("SnowPlane") as GameObject;

        grid = new List<GameObject>()
        {
            Instantiate(snowPlaneResource, new Vector3(0f, 0f, 0f), Quaternion.identity),
            Instantiate(snowPlaneResource, new Vector3(d, 0f, 0f), Quaternion.identity),
            Instantiate(snowPlaneResource, new Vector3(-d, 0f, 0f), Quaternion.identity),

            Instantiate(snowPlaneResource, new Vector3(0f, 0f, d), Quaternion.identity),
            Instantiate(snowPlaneResource, new Vector3(d, 0f, d), Quaternion.identity),
            Instantiate(snowPlaneResource, new Vector3(-d, 0f, d), Quaternion.identity),

            Instantiate(snowPlaneResource, new Vector3(0f, 0f, -d), Quaternion.identity),
            Instantiate(snowPlaneResource, new Vector3(d, 0f, -d), Quaternion.identity),
            Instantiate(snowPlaneResource, new Vector3(-d, 0f, -d), Quaternion.identity),
        };

        oldForest = grid[0].transform;

        foreach (GameObject plane in grid)
            plane.GetComponent<PlaneController>().PopulatePlane(forest);

        navmesh.BuildNavMesh();
    }

    void Update()
    {
        // HACK to prevent uneven number of trigger function's
        if (changeEnter != changeExit)
        {
            changeEnter = true;
            changeExit = true;
        }
        
        //updates the grid
        if (changeEnter & changeExit)
        {
            infWalk(oldForest.position.x, oldForest.position.z, currentForest.position.x, currentForest.position.z);
            changeEnter = false;
            changeExit = false;

            // rebuild nav mesh surface
            navmesh.BuildNavMesh();
        }
    }

    //keeps the player in the forest no matter where they walk
    void infWalk(float X, float Z, float newX, float newZ)
    {
        if (newX - X == d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.x == X - d)
                {
                    grid[i].transform.position += new Vector3(3 * d, 0, 0);
                    grid[i].gameObject.GetComponent<PlaneController>().PopulatePlane(forest);
                }
            }
        }

        if (newX - X == -d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.x == X + d)
                {
                    grid[i].transform.position -= new Vector3(3 * d, 0, 0);
                    grid[i].gameObject.GetComponent<PlaneController>().PopulatePlane(forest);
                }       
            }
        }

        if (newZ - Z == d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.z == Z - d)
                {
                    grid[i].transform.position += new Vector3(0, 0, 3 * d);
                    grid[i].gameObject.GetComponent<PlaneController>().PopulatePlane(forest);
                }     
            }
        }

        if (newZ - Z == -d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.z == Z + d)
                {
                    grid[i].transform.position -= new Vector3(0, 0, 3 * d);
                    grid[i].gameObject.GetComponent<PlaneController>().PopulatePlane(forest);
                }                  
            }
        }

        //keep track of planes that were moved
        //repopulate them by calling function in plane controller
    }
}
