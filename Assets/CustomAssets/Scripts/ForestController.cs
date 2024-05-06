using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    public bool changeEnter;
    public bool changeExit;
    float d = 100f;

    public List<GameObject> grid = new List<GameObject>();

    public Transform oldForest;
    public Transform currentForest;

    GameObject snowPlaneResource;

    void Start()
    {
        changeEnter = false;
        changeExit = false;
    }

    void Update()
    {
        //dirty fix to prevent uneven number of trigger fun.'s
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
            foreach (GameObject plane in grid)
            {
                if (plane.transform.position.x == X - d)
                {
                    plane.transform.position += new Vector3(3 * d, 0, 0);

                    //instead remove from list and destroy plane
                    //spawn new plane at position and add to grid list
                } 
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
    }
}
