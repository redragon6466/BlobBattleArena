using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    protected BlobScript creator;
    protected BlobScript target;
    protected int speed;
    protected int state;
    protected int timeToLive = 60;
    // Start is called before the first frame update
    void Start()
    {
        state = 1;
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



    // Update is called once per frame
    void Update()
    {
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

    }
    public void setRotation()
    {
        float TargetLocX;
        float TargetLocY;
        float AttackerLocX;
        float AttackerLocY;

        float opp = TargetLocY - AttackerLocY;
        float hyp = (float)Math.Sqrt(Math.Pow(TargetLocY - AttackerLocX,(float)2) + Math.Pow(TargetLocX - AttackerLocX,(float)2));




    }


    private void CleanUp()
    {
        Destroy(this.gameObject);
    }
}
