using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class BaseBrain : IBrain
    {

        public virtual void TakeTurn(BlobScript me, BlobScript[] allyBlobs, BlobScript[] enemyBlobs)
        {
            
        }

        /// <summary>
        /// Get the length of it, L, and multiply all components by M/L, where M is the new length of the vector.
        /// </summary>
        /// <param name="myPos"></param>
        /// <param name="otherPos"></param>
        /// <param name="moveSpeed"></param>
        /// <returns></returns>
        public Vector3 MoveTo(Vector2 myPos, Vector2 otherPos, float moveSpeed)
        {

            Debug.Log(Vector2.Distance(myPos, otherPos));



            if (Vector3.Distance(myPos, otherPos) > moveSpeed)
            {
                var temp1 = Vector3.Distance(myPos, otherPos);
                var xdif =  otherPos.x - myPos.x;
                var ydif =  otherPos.y - myPos.y;

                return new Vector2(myPos.x + moveSpeed / temp1 * xdif, myPos.y + moveSpeed / temp1 * ydif);
            }
            else
            {
                return otherPos;
            }


        }

        public BlobScript ClostedBlob(BlobScript myPos, List<BlobScript> otherPositions)
        {
            var distances = DistancesToMulti(myPos, otherPositions);

            return distances.FirstOrDefault().Item2;
        }

        public BlobScript FarthestBlob(BlobScript myPos, List<BlobScript> otherPositions)
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
        public List<Tuple<float, BlobScript>> DistancesToMulti(BlobScript myPos, List<BlobScript> otherPositions)
        {
            List<Tuple<float, BlobScript>> distances = new List<Tuple<float, BlobScript>>();

            foreach (var pos in otherPositions)
            {
                distances.Add(new Tuple<float, BlobScript>(Vector3.Distance(myPos.transform.position, pos.transform.position), pos));
            }


            return distances.OrderBy(x => x.Item1).ToList();
        }

        public float CalculateDistance(Vector3 myPos, Vector3 otherPos)
        {
            return Mathf.Pow(Mathf.Pow(otherPos.x - myPos.x, 2f) + Mathf.Pow(otherPos.y - myPos.y, 2f) + Mathf.Pow(otherPos.z - myPos.z, 2f), .5f);
        }
    }
}
