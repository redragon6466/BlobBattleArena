using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : BaseAttack
{

    public bool fireAttack(BlobScript attacker, BlobScript target)
    {
        //Not done, needs a lot of work.
        if (true)
        //replace above statement with a range checker
        {
            int dmg = attacker.GetAttack() - target.GetDefense();
            if (dmg < 0)
            {
                dmg = 0;
            }
            //Debug.Log(dmg);
            target.TakeDamage(dmg);
        }

        return true;
    }
}
