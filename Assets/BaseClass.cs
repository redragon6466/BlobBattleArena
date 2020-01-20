using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class BaseClass : IClass
    {
        public int Health { get; protected set; }

        public int Attack { get; protected set; }

        public int Defense { get; protected set; }

        public int Initiative { get; protected set; }

        public int Movement { get; protected set; }

        public virtual void TakeTurn(GameObject[] allyBlobs, GameObject[] enemyBlobs)
        {
            
        }
    }
}
