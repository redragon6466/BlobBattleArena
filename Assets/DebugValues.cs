using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Services;

public class DebugValues : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool mousePos;
    [SerializeField]
    private bool gridPos;
    private string displayText;

    void Start()
    {
        
    }
    public void buildLine()
    {
        displayText = "";
        Vector3 mouseLOC = Input.mousePosition;
        if (mousePos)
        {
            //add a line of text for mouse pos to display text
            //Get Scene Camera
            //Get X/Y, convert to 
            


            displayText += "Mouse X: " + (int)mouseLOC.x + " Mouse Y: " + (int)mouseLOC.y + " \n";
        }
        if(gridPos)
        {
            displayText += "Grid X/Y:" + GridService.Instance.PointToGrid( (int)mouseLOC.x, (int)mouseLOC.y );
        }

    }



    // Update is called once per frame
    void Update()
    {
        buildLine();
        Text myText = this.GetComponentInChildren<Text>();
        myText.text = displayText;
    }
}
