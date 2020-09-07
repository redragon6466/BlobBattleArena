using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Services;

public class DisplayGrid : MonoBehaviour
{
    private List<List<GameObject>> grid;
    int xMax = 20;
    int yMax = 20;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject StartObject;
    bool GridCreated = false;

    void Start()
    {

        
    }
    public void MakeNewGrid()
    {
        GridCreated = true;
        for (int xStart = 0; xStart <= xMax; xStart += 1)
        {
            Debug.Log("CURX = " + xStart);
            for (int yStart = 0; yStart <= yMax; yStart += 1)
            {
                Debug.Log("CURY = " + yStart);
                GameObject ClonedPointObject = Instantiate(StartObject);
                //Take object
                //Make a call to the grid for X/Y
                //Set Object Postion to location for X/Y

                Debug.Log("KAPPA CREATED");
                //grid[xStart][yStart] = ClonedPointObject;
            }
        }
        //GridCreated = true;
        //GridCreated = true;
    }
    public bool HaveCreatedGrid()
    {
        return GridCreated;
    }
    // Update is called once per frame
    void Update()
    {
        if(!GridCreated)
        {
            MakeNewGrid();
        }
    }
}
