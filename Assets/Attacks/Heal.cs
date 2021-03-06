﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Heal : BaseAttack
    {

        private new readonly int attackRange = 30;

        public override bool FireSpell(BlobScript source, BlobScript target)
        {
            //Debug.Log("Firing Punch Attack");
            //Not done, needs a lot of work.
            if (true)
            //replace above statement with a range checker
            {
                ShowAttack(target, source);
                int dmg = source.GetSpecialAttack();
                if (dmg < 0)
                {
                    dmg = 0;
                }
                //Debug.Log(dmg);
                target.RestoreHealth(dmg);
            }



            return true;
        }


        //clean up parent/attacker references
        public void ShowAttack(BlobScript target, BlobScript source)
        {
            //var NewArrow = Instantiate()
            var pointyThing = (GameObject)Resources.Load("Prefabs/HealyArrow", typeof(GameObject));
            //Debug.Log(pointyThing);
            var myThing = UnityEngine.Object.Instantiate(pointyThing, new Vector3(source.transform.position.x, source.transform.position.y, source.transform.position.z - 1), Quaternion.identity);
            ArrowScript myScript = myThing.GetComponent<ArrowScript>();
            myScript.setTargetAndParent(source, target, this, 0);
        }

    }
}

