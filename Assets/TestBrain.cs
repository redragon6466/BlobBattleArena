using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class TestBrain : BaseBrain
    {

        public override void TakeTurn(BlobScript me, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            me.transform.position = new Vector3(me.transform.position.x + 3, me.transform.position.y);
        }
    }
}
