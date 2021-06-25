using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class HealerClass : BaseClass
    {

        public HealerClass()
        {
            Health = 200;
            Attack = 20;
            SpecialAttack = 30;
            Defense = 10;
            Initiative = 100; // TESTING TODO FIX
            Movement = 5;

            AttackList = new List<BaseAttack> { new PunchAttack(), new Heal() };

        }


        public override string ToString()
        {
            return "Healer";
        }

    }
}
