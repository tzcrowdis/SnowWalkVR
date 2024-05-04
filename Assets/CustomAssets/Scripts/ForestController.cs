using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    public bool changeEnter;
    public bool changeExit;
    private float d = 100f;
    private Vector3 newPos;

    public GameObject[] grid;

    public Transform oldGrid;
    public Transform newGrid;

    void Start()
    {
        changeEnter = false;
        changeExit = false;
        oldGrid = GameObject.Find("woods0").transform;

        grid = new GameObject[9];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = GameObject.Find(string.Format("woods{0}", i));
        }
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
        if (changeEnter == true && changeExit == true)
        {
            infWalk(oldGrid.position.x, oldGrid.position.z, newGrid.position.x, newGrid.position.z);
            changeEnter = false;
            changeExit = false;
        }
    }

    //keeps the player in the forest no matter where they walk
    void infWalk(float X, float Z, float newX, float newZ)
    {
        if (newX - X == d)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i].transform.position.x == X - d)
                    grid[i].transform.position += new Vector3(3*d, 0, 0);
            }
        }

        if (newX - X == -d)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i].transform.position.x == X + d)
                    grid[i].transform.position -= new Vector3(3*d, 0, 0);
            }
        }

        if (newZ - Z == d)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i].transform.position.z == Z - d)
                    grid[i].transform.position += new Vector3(0, 0, 3*d);
            }
        }

        if (newZ - Z == -d)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                if (grid[i].transform.position.z == Z + d)
                    grid[i].transform.position -= new Vector3(0, 0, 3*d);
            }
        }
    }
}
