using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : BaseAttack
{

    public override bool FireAttack(BlobScript attacker, BlobScript target)
    {
        
        target.TakeDamage(attackDamage);


        return true;
    }

}