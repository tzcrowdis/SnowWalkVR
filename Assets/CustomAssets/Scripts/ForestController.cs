using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    public bool changeEnter;
    public bool changeExit;
    public float d = 50f; //side length of snow plane

    public List<GameObject> grid;

    public Transform oldForest;
    public Transform currentForest;

    public float uniformForestDensity;

    GameObject snowPlaneResource;

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
            plane.GetComponent<PlaneController>().PopulatePlane("UniformForest");
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
                    grid[i].transform.position += new Vector3(3 * d, 0, 0);
            }
        }

        if (newX - X == -d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.x == X + d)
                    grid[i].transform.position -= new Vector3(3*d, 0, 0);
            }
        }

        if (newZ - Z == d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.z == Z - d)
                    grid[i].transform.position += new Vector3(0, 0, 3*d);
            }
        }

        if (newZ - Z == -d)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].transform.position.z == Z + d)
                    grid[i].transform.position -= new Vector3(0, 0, 3*d);
            }
        }

        //keep track of planes that were moved
        //repopulate them by calling function in plane controller
    }
}
