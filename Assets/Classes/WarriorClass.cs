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
            SpecialAttack = 0;
            Defense = 10;
            Initiative = 10;
            Movement = 5;


            AttackList = new List<BaseAttack> { new PunchAttack()};
        }


        public override string ToString()
        {
            return "Warrior";
        }

    }
}
