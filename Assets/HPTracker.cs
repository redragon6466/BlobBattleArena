using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class HPTracker : MonoBehaviour
{
    BlobScript myParent;
    bool hasParent;
    Text myText;
    Slider _healthBar;
    GameObject parentBlob;

    // Start is called before the first frame update
    void Start()
    {
        //myText = this.GetComponent<Text>();
        //myText.text = "potato";
        //_healthBar = this.GetComponent<Slider>();

        setMyParent();
        

        
    }

    public bool setMyParent()
    {
        //get parent canvas
        GameObject parentCanvas = this.transform.parent.gameObject;
        //get parent gameobject
        parentBlob = parentCanvas.transform.parent.gameObject;
        
        //get attached script
        myParent = parentBlob.GetComponent<BlobScript>();
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = parentBlob.transform.position;
        //myText.text = myParent.GetHealth().ToString();
        //_healthBar.value = myParent.GetHealth();
    }
}
