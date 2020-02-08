using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class GuardianBrain : BaseBrain
    {
        private BlobScript _me;
        private List<BlobScript> _allyBlobs;
        private List<BlobScript> _enemyBlobs;

        public override void TakeTurn(BlobScript source, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            _me = source;
            _allyBlobs = allyBlobs;
            _enemyBlobs = enemyBlobs;

            //if there are no allies to protect behave like a warrior
            if (!PriorityTargetsToDefend())
            {
                var a = new PunchAttack();
                var targets = a.GetPossibleTargets(source, enemyBlobs.ToList());
                if (targets.Count > 0)
                {
                    a.FireAttack(source, targets.First());
                    return;
                }

                var target = DetermineTarget();

                source.transform.position = MoveTo(source.transform.position, target.transform.position, 5f);
            }

            var moveTo = DetermineAverageOfPos();

            source.transform.position = MoveTo(source.transform.position, moveTo, 5f);


            UnityEngine.Object.FindObjectOfType<God>().EndTurn();
        }

        public override BrainEnum GetBrainType()
        {
            return BrainEnum.Guardian;
        }

        private bool PriorityTargetsToDefend()
        {
            if (_allyBlobs.Count == 0)
            {
                return true;
            }
            foreach (var blob in _allyBlobs)
            {
                if (blob.GetBrainType() == BrainEnum.Healer) // Guardians protect healers
                {
                    return true;
                }
            }

            return  false;
        }

        private BlobScript DetermineTarget()
        {
            return ClostedBlob(_me, _enemyBlobs.ToList());
        }

        private Vector2 DetermineAverageOfPos()
        {
            var temp = _allyBlobs.ToList();
            temp.AddRange(_enemyBlobs);

            int count = 0;
            float xSum = 0;
            float ySum = 0;
            foreach (var blob in temp)
            {
                xSum += blob.transform.position.x;
                ySum += blob.transform.position.y;
                count++;
            }

            return new Vector2(xSum / count, ySum / count);

        }
    }
}
