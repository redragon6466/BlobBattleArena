using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;


public class ShowButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string buttonName;
    BlobScript parentScript;
    bool parentFound;
    Text thisButtonText;
    [SerializeField]
    GameObject thisCanvas;
    

    void Start()
    {
        parentScript = this.GetComponentInParent<BlobScript>();
        if(parentScript != null)
        {
            parentFound = true;
            thisButtonText = this.GetComponentInChildren<Text>();
            if(buttonName != null && thisButtonText != null)
            {
                thisButtonText.text = buttonName;

            }
            //thisButtonText.text = buttonName;
            
        }
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        //Destroy(this.gameObject);
        Debug.Log(buttonName);
    }
    void GridUp()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(parentFound)
        {
            if(parentScript.CheckDebugMenu())
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                thisCanvas.SetActive(true);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                thisCanvas.SetActive(false);
            }
        }
    }
}
