using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Attacks.UltAttacks
{
    public class WhirlwindStrikeUlt : BaseAttack
    {

        public override bool FireAttack(BlobScript source, List<BlobScript> targets)
        {
            //Debug.Log("Firing Punch Attack");
            //Not done, needs a lot of work.
            foreach (var target in targets)
            {
                ShowAttack(target, source);
                int dmg = source.GetAttack() - target.GetDefense();
                if (dmg < 0)
                {
                    dmg = 0;
                }
                //Debug.Log(dmg);
                target.TakeDamage(dmg);
                source.ChargeUlt(dmg, 0);
            }
            

            return true;
        }

        //clean up parent/attacker references
        //this should be moved into base attack, with the arrow being passed in as a references
        public void ShowAttack(BlobScript target, BlobScript source)
        {
            //var NewArrow = Instantiate()
            var pointyThing = (GameObject)Resources.Load("Prefabs/PointyArrow", typeof(GameObject));
            //Debug.Log(pointyThing);
            var myThing = UnityEngine.Object.Instantiate(pointyThing, new Vector3(source.transform.position.x, source.transform.position.y, source.transform.position.z - 1), Quaternion.identity);
            ArrowScript myScript = myThing.GetComponent<ArrowScript>();
            myScript.setTargetAndParent(source, target, this, 0);
        }
    }
}
