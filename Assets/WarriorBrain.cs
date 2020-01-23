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
        private List<BlobScript> _allyBlobs; 
        private List<BlobScript> _enemyBlobs;

        public override void TakeTurn(BlobScript me, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            _me = me;
            _allyBlobs = allyBlobs;
            _enemyBlobs = enemyBlobs;

            var a = new PunchAttack();
            var targets = a.GetPossibleTargets(me, enemyBlobs.ToList());
            if (targets.Count > 0)
            {
                a.FireAttack(me, targets.First());
                return;
            }

            var target = DetermineTarget();
            
            me.transform.position = MoveTo(me.transform.position, target.transform.position, 5f);
        }

        private BlobScript DetermineTarget()
        {
            return ClostedBlob(_me, _enemyBlobs.ToList());
        }



    }
}
