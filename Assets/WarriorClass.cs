using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class WarriorClass : BaseClass
    {

        public WarriorClass()
        {
            Health = 300;
            Attack = 30;
            Defense = 10;
            Initiative = 10;
            Movement = 5;

        }

        public override void TakeTurn(GameObject[] allyBlobs, GameObject[] enemyBlobs)
        {
            //do stuff
        }

    }
}
