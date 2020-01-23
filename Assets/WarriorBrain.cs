using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class WarriorBrain : BaseBrain
    {
        private BlobScript _me; 
        private BlobScript[] _allyBlobs; 
        private BlobScript[] _enemyBlobs;

        public override void TakeTurn(BlobScript me, BlobScript[] allyBlobs, BlobScript[] enemyBlobs)
        {
            _me = me;
            _allyBlobs = allyBlobs;
            _enemyBlobs = enemyBlobs;
            var target = DetermineTarget();
            Debug.Log(string.Format("Move from: {0}, To: {1}", me.transform.position, target.transform.position));
            me.transform.position = MoveTo(me.transform.position, target.transform.position, 5f);
        }

        private BlobScript DetermineTarget()
        {
            return ClostedBlob(_me, _enemyBlobs.ToList());
        }

    }
}
