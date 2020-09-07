using Assets.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class BaseAttack
    {
        protected List<BlobScript> possibleTargets = new List<BlobScript>();
        //attacks range
        protected int attackRange = 1;
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
            foreach (BlobScript thisBlob in possibleTargets)
            {
                if (!InRange(myBlob, thisBlob))
                {
                    bool removeSuccess = tempTargets.Remove(thisBlob);
                    if (!removeSuccess)
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
            return false;
        }
        //Returns true if attack successfully fires. Returns false if target is illegal, or attack fails for some reason. 
        public virtual bool FireAttack(BlobScript attacker, List<BlobScript> targets)
        {
            //Not done, needs a lot of work.
            return false;
        }

        //Returns true if attack successfully fires. Returns false if target is illegal, or attack fails for some reason. 
        public virtual bool FireSpell(BlobScript attacker, BlobScript target)
        {
            return false;
        }

        public bool InRange(BlobScript attacker, BlobScript target)
        {
            var attackerLoc = attacker.GetGridLocation();
            var targetLoc = target.GetGridLocation();
            var xdis = Math.Abs(targetLoc.x - attackerLoc.x);
            var ydis = Math.Abs(targetLoc.y - attackerLoc.y);

            if (xdis > ydis)
            {
                return xdis <= attackRange;
            }

            if (ydis > xdis)
            {
                return ydis <= attackRange;
            }

            return ydis <= attackRange;

        }

        public bool InRangeC(BlobScript attacker, BlobScript target)
        {
            var attackerLoc = attacker.GetGridLocation();
            var targetLoc = target.GetGridLocation();
            var xdis = Math.Pow(targetLoc.x - attackerLoc.x, 2);
            var ydis = Math.Pow(targetLoc.y - attackerLoc.y, 2);

            var c = (int)Math.Sqrt(xdis + ydis);

            //for close distances calculating the hypotenuse is a resonable approximation of diagonals counting as 1 square
            if (attackRange <= 2 && c <= attackRange)
            {
                return true;
            }

            return GridService.Instance.GetDistance(attackerLoc, targetLoc) <= attackRange;

        }



        // Update is called once per frame
        // void Update()
        // {

        // }
    }
}

