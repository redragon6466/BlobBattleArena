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

        public int SpecialAttack { get; protected set; }

        public int Defense { get; protected set; }

        public int Initiative { get; protected set; }

        public int Movement { get; protected set; }

        public List<BaseAttack> AttackList { get; protected set; }

        public virtual void TakeTurn(GameObject[] allyBlobs, GameObject[] enemyBlobs)
        {
            
        }

        /// <summary>
        /// Get the length of it, L, and multiply all components by M/L, where M is the new length of the vector.
        /// </summary>
        /// <param name="myPos"></param>
        /// <param name="otherPos"></param>
        /// <param name="moveSpeed"></param>
        /// <returns></returns>
        public Vector3 MoveTo(Vector3 myPos, Vector3 otherPos, int moveSpeed)
        {
            if ((otherPos - myPos).magnitude > moveSpeed)
            {
                var temp1 =  myPos.magnitude + moveSpeed;
                return new Vector3(myPos.x * temp1 / myPos.magnitude, myPos.y * temp1 / myPos.magnitude, myPos.z * temp1 / myPos.magnitude);
            }
            else
            {
                return otherPos;
            }
        }

        public Vector3 ClostedBlob(Vector3 myPos, List<Vector3> otherPositions)
        {
            var distances = DistancesToMulti(myPos, otherPositions);

            return distances.FirstOrDefault().Item2;
        }

        public Vector3 FarthestBlob(Vector3 myPos, List<Vector3> otherPositions)
        {
            var distances = DistancesToMulti(myPos, otherPositions);

            return distances.LastOrDefault().Item2;
        }

        /// <summary>
        /// Gets a sorted list of distnaces from a position
        /// </summary>
        /// <param name="myPos"></param>
        /// <param name="otherPositions"></param>
        /// <returns></returns>
        public List<Tuple<float, Vector3>> DistancesToMulti(Vector3 myPos, List<Vector3> otherPositions)
        {
            List<Tuple<float, Vector3>> distances = new List<Tuple<float, Vector3>>();

            foreach (var pos in otherPositions)
            {
                distances.Add(new Tuple<float, Vector3>(CalculateDistance(myPos, pos), pos));
            }


            return distances.OrderBy(x => x.Item1).ToList();
        }

        public float CalculateDistance(Vector3 myPos, Vector3 otherPos)
        {
            return Mathf.Pow(Mathf.Pow(otherPos.x - myPos.x, 2f) + Mathf.Pow(otherPos.y - myPos.y, 2f) + Mathf.Pow(otherPos.z - myPos.z, 2f), .5f); 
        }
    }
}
