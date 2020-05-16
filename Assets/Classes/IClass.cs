using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public interface IClass
    {
        int Health { get;  }

        int Attack { get;  }

        int Defense { get;  }

        int Initiative { get; }

        int Movement { get;  }

        void TakeTurn(GameObject[] allyBlobs, GameObject[] enemyBlobs);

    }
}
