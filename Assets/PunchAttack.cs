﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : BaseAttack
{
     
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool fireAttack(BlobScript attacker, BlobScript target)
    {
        //Not done, needs a lot of work.
        if(true)
        //replace above statement with a range checker
        {
            int dmg = attacker.GetAttack() - target.GetDefense();
            if(dmg < 0)
            {
                dmg = 0;
            }
            target.TakeDamage(dmg);
        }

        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
