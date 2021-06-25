using Assets.Services;
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

            var criticalList = new List<BlobScript>();
            var lowList = new List<BlobScript>();

            //prioritize low health allies
            /*
            foreach (var ally in _allyBlobs)
            {
                if (ally.GetHealth() < 50)
                {
                    criticalList.Add(ally);
                }
                else if (ally.GetHealth() < 150)
                {
                    lowList.Add(ally);
                }
            }
            Debug.Log(criticalList.Count);
            if (criticalList.Count > 0)
            {
                foreach (var criticalAlly in criticalList)
                {
                    if (_me.MoveSet().ElementAt(1).InRange(_me, criticalAlly))
                    {
                        _me.MoveSet().ElementAt(1).FireSpell(source, criticalList.First());
                        return;
                    }
                }
                var close = FindClosestAlly(criticalList);



            }

            if (EnemyInRange())
            {

            }

            var target = DetermineTarget();*/

           var safe =  MovementService.Instance.FindSafestInRange(_me, _enemyBlobs, _me.GetMovement());

            source.transform.position = MoveTo(source.transform.position, safe, 5f);
            //source.GetComponent<Rigidbody2D>().MovePosition(MoveTo(source.transform.position, target.transform.position, 5f));

            UnityEngine.Object.FindObjectOfType<God>().EndTurn();
        }

        public override BrainEnum GetBrainType()
        {
            return BrainEnum.Healer;
        }

        private BlobScript DetermineTarget()
        {
            return ClostedBlob(_me, _enemyBlobs.ToList());
        }

        private BlobScript DetermineBlobTarget(BlobScript blob)
        {
            return ClostedBlob(blob, _enemyBlobs.ToList());
        }

        private bool EnemyInRange()
        {
            foreach (var enemy in _enemyBlobs)
            {
                if (enemy.IsInRange(_me))
                {
                    return true;
                }
            }
            return false;

        }

        private BlobScript FindClosestAlly()
        {
            var list = _allyBlobs.Where(x => x.GetBrainType() != BrainEnum.Healer);

            return ClostedBlob(_me, list.ToList());
        }


        private BlobScript FindClosestAlly(List<BlobScript> allies)
        {
            return ClostedBlob(_me, allies.ToList());
        }
    }
}

