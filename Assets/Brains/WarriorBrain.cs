using Assets.Services;
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

        public override void TakeTurn(BlobScript source, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            _me = source;
            _allyBlobs = allyBlobs;
            _enemyBlobs = enemyBlobs;

            var a = new PunchAttack();
            var targets = a.GetPossibleTargets(source, enemyBlobs.ToList());
            if (targets.Count > 0)
            {
                a.FireAttack(source, targets.First());
                return;
            }

            var close = 1000;
            var x = 0;
            var y = 0;
            var target = DetermineTarget();
            var loc = target.GetGridLocation();
            var myLoc = _me.GetGridLocation();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (GridService.Instance.SpaceOccupied((int)loc.x + i, (int)loc.y + j) || !GridService.Instance.IsInGrid((int)loc.x + i, (int)loc.y + j))
                    {
                        continue;
                    }
                    var dis = GridService.Instance.GetDistance(myLoc, new Vector2(loc.x + i, loc.y + j));    
                    if (dis < close && dis != 0)
                    {
                         close = dis;
                         x = (int)loc.x + i;
                         y = (int)loc.y + j;
                    }  
                }
            }
            var newGridLoc = MoveTo(myLoc, new Vector2(x, y), 5f);
            source.SetGridLocation((int)newGridLoc.x, (int)newGridLoc.y);
            source.transform.position = GridService.Instance.ConvertToPoint(newGridLoc);
            //source.GetComponent<Rigidbody2D>().MovePosition(MoveTo(source.transform.position, target.transform.position, 5f));

           God.Instance.EndTurn();
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
