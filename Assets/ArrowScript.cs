using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    protected BlobScript creator;
    protected BlobScript target;
    protected int speed;
    protected int state = 1;
    protected int timeToLive = 180;
    // Start is called before the first frame update
    void Start()
    {
        //state = 1;
        //Debug.Log("Arrow Created");

    }
    /**
     * States 
     * 1 - Just created, no values yet.
     * 2 - Target and parent set
     * 3 - Destroy it and clean up
     */
    public bool setState(int stateValue)
    {
        state = stateValue;
        return true;
    }
    public void setTargetAndParent(BlobScript parent, BlobScript enemy)
    {
        creator = parent;
        target = enemy;
       
        setState(2);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        if(state == 1)
        {
            //do nothing
        }
        else if(state == 2)
        {
            MoveToTarget();
        }
        else if(state == 3)
        {
            timeToLive--;
            GetComponent<Rigidbody2D>().velocity = transform.up * 50 * Time.deltaTime;
            if(timeToLive <= 0)
            {
                setState(4);
            }
        }
        else if(state == 4)
        {
            CleanUp();
        }
        else
        {
            Debug.Log("Arrow Script, Bad State Value");
        }
    }

    private void MoveToTarget()
    {
        setRotation();
        setState(3);
    }
    public void setRotation()
    {
        float TargetLocX;
        float TargetLocY;
        float AttackerLocX;
        float AttackerLocY;
        if(target.gameObject == null || creator.gameObject == null)
        {
            setState(4);
            return;
        }



        TargetLocX = target.gameObject.transform.position.x;
        TargetLocY = target.gameObject.transform.position.y;
        AttackerLocX = creator.gameObject.transform.position.x;
        AttackerLocY = creator.gameObject.transform.position.y;


        float opp = TargetLocY - AttackerLocY;
        float hyp = (float)Math.Sqrt(Math.Pow(TargetLocY - AttackerLocX,(float)2) + Math.Pow(TargetLocX - AttackerLocX,(float)2));
        float sine = opp / hyp;
        Debug.Log(sine);
        this.transform.Rotate(0, 0, sine * (float)(180/Math.PI), Space.World);



    }


    private void CleanUp()
    {
        Destroy(this.gameObject);
    }
}
