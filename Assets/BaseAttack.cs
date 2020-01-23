using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    List<BlobScript> possibleTargets = new List<BlobScript>();
    //attacks range
    int attackRange;
    //exclude self from targets?
    bool excludeSelf;
    BlobScript myBlob;
    //Enter attack
    //Send back targets
    //Select Primary Target or Cancel
    //Fire attack
    public List<BlobScript> getPossibleTargets(BlobScript attacker)
    {
        //Empty target list
        possibleTargets = new List<BlobScript>();
        //get input attacker
        myBlob = attacker;
        //Where are they?
        //Get the blobs location
        //What is my range?
        
        //Search map for targets
        foreach(BlobScript thisBlob in possibleTargets)
        {
            if( !(calcDistince(myBlob, thisBlob) <= attackRange) )
            {
                bool removeSuccess = possibleTargets.Remove(thisBlob);
                if(!removeSuccess)
                {
                    Debug.Log("Failed to remove blobscript, " + thisBlob + "from attack targets. Error may have occured.");
                }

            }

        }
        return possibleTargets;
    }
    private List<BlobScript> getMap()
    {
        return new List<BlobScript>();
    }
    private int calcDistince(BlobScript attacker, BlobScript target)
    {
        //calculate range between blob attacker, and blob target
        return 1;
    }
    //Returns true if attack successfully fires. Returns false if target is illegal, or attack fails for some reason. 
    public bool fireAttack(BlobScript attacker, BlobScript target)
    {
        //Not done, needs a lot of work.


        return true;
    }
    

    // Update is called once per frame
   // void Update()
   // {
        
   // }
}
