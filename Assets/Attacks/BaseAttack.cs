using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack 
{
    protected List<BlobScript> possibleTargets = new List<BlobScript>();
    //attacks range
    protected const int attackRange = 3;
    protected const int attackDamage = 100; //TODO attackes should have different damage
    //exclude self from targets?
    protected bool excludeSelf;
    protected BlobScript myBlob;
    //Enter attack
    //Send back targets
    //Select Primary Target or Cancel
    //Fire attack
    public List<BlobScript> GetPossibleTargets(BlobScript attacker, List<BlobScript> pTargets)
    {
        //Empty target list
        possibleTargets = pTargets;
        var tempTargets = new List<BlobScript>(pTargets);
        //get input attacker
        myBlob = attacker;
        //Where are they?
        //Get the blobs location
        //What is my range?
        
        //Search map for targets
        foreach(BlobScript thisBlob in possibleTargets)
        {
            if( !InRange(myBlob, thisBlob) )
            {
                bool removeSuccess = tempTargets.Remove(thisBlob);
                if(!removeSuccess)
                {
                    Debug.Log("Failed to remove blobscript, " + thisBlob + "from attack targets. Error may have occured.");
                }

            }

        }
        possibleTargets = tempTargets; //TODO fritz is this the expected behavior of this method? I modefied it a touch
        
        return tempTargets;
    }
    protected List<BlobScript> GetMap()
    {
        return new List<BlobScript>();
    }
    protected float calcDistince(BlobScript attacker, BlobScript target)
    {
        //calculate range between blob attacker, and blob target
        return Vector3.Distance(attacker.transform.position, target.transform.position);
    }
    //Returns true if attack successfully fires. Returns false if target is illegal, or attack fails for some reason. 
    public virtual bool FireAttack(BlobScript attacker, BlobScript target)
    {
        //Not done, needs a lot of work.
        target.TakeDamage(attackDamage);
        
        return true;
    }

    //Returns true if attack successfully fires. Returns false if target is illegal, or attack fails for some reason. 
    public virtual bool FireSpell(BlobScript attacker, BlobScript target)
    {
        //Not done, needs a lot of work.
        target.TakeDamage(attackDamage);

        return true;
    }

    protected bool InRange(BlobScript attacker, BlobScript target)
    {
        return calcDistince(attacker, target) <= attackRange;



    }



    // Update is called once per frame
    // void Update()
    // {

    // }
}
