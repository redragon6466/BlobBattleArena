using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class GuardianClass : BaseClass
    {

        public GuardianClass()
        {
            Health = 0;
            Attack = 20;
            SpecialAttack = 0;
            Defense = 20;
            Initiative = 10;
            Movement = 5;


            AttackList = new List<BaseAttack> { new PunchAttack()};
        }

        public override string ToString()
        {
            return "Guardian";
        }

    }
}
