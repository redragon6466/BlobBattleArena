using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class HealerBrain : BaseBrain
    {
        private BlobScript _me; 
        private List<BlobScript> _allyBlobs; 
        private List<BlobScript> _enemyBlobs;

        public override void TakeTurn(BlobScript source, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            _me = source;
            _allyBlobs = allyBlobs;
            _enemyBlobs = enemyBlobs;

            var a = new PunchAttack();
            var h = new Heal();
            var targets = a.GetPossibleTargets(source, enemyBlobs.ToList());
            if (targets.Count > 0)
            {
                a.FireAttack(source, targets.First());
                return;
            }

            var target = DetermineTarget();

            source.transform.position = MoveTo(source.transform.position, target.transform.position, 5f);
            //source.GetComponent<Rigidbody2D>().MovePosition(MoveTo(source.transform.position, target.transform.position, 5f));

            UnityEngine.Object.FindObjectOfType<God>().EndTurn();
        }

        public override BrainEnum GetBrainType()
        {
            return BrainEnum.Warrior;
        }

        private BlobScript DetermineTarget()
        {
            return ClostedBlob(_me, _enemyBlobs.ToList());
        }



    }
}
