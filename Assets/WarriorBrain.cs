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
        private GameObject _me; 
        private GameObject[] _allyBlobs; 
        private GameObject[] _enemyBlobs;

        public override void TakeTurn(GameObject me, GameObject[] allyBlobs, GameObject[] enemyBlobs)
        {
            _me = me;
            _allyBlobs = allyBlobs;
            _enemyBlobs = enemyBlobs;
            var target = DetermineTarget();
            Debug.Log(string.Format("Move from: {0}, To: {1}", me.transform.position, target.transform.position));
            me.transform.position = MoveTo(me.transform.position, target.transform.position, .5f);
        }

        private GameObject DetermineTarget()
        {
            return ClostedBlob(_me, _enemyBlobs.ToList());
        }

    }
}
