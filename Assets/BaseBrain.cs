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

        public virtual void TakeTurn(BlobScript me, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            
        }

        public virtual BrainEnum GetBrainType()
        {
            return BrainEnum.Test;
        }

        /// <summary>
        /// Get the length of it, L, and multiply all components by M/L, where M is the new length of the vector.
        /// </summary>
        /// <param name="myPos"></param>
        /// <param name="otherPos"></param>
        /// <param name="moveSpeed"></param>
        /// <returns></returns>
        public Vector3 MoveTo(Vector2 sourcePos, Vector2 targetPos, float moveSpeed)
        {
            if (Vector3.Distance(sourcePos, targetPos) > moveSpeed)
            {
                var temp1 = Vector3.Distance(sourcePos, targetPos);
                var xdif = targetPos.x - sourcePos.x;
                var ydif = targetPos.y - sourcePos.y;

                return new Vector2(sourcePos.x + moveSpeed / temp1 * xdif, sourcePos.y + moveSpeed / temp1 * ydif);
            }
            else
            {
                var temp1 = Vector3.Distance(sourcePos, targetPos);
                var xdif = sourcePos.x - targetPos.x;
                var ydif = sourcePos.y - targetPos.y;


                return new Vector2(targetPos.x + 1 / temp1 * xdif, targetPos.y + 1 / temp1 * ydif);
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
