using Assets.Services;
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
        private const int _blobDistance = 4; // The distance where two blobs are next to each other
        public virtual void TakeTurn(BlobScript me, List<BlobScript> allyBlobs, List<BlobScript> enemyBlobs)
        {
            
        }

        public virtual BrainEnum GetBrainType()
        {
            return BrainEnum.Test;
        }


        protected virtual async void TrackAttacks()
        {
            //ovverride in child class
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
            //if (GridService.Instance.GetDistance(sourcePos, targetPos) < _blobDistance)
            //{
               // return new Vector3(sourcePos.x, sourcePos.y);
            //}
            
            if (GridService.Instance.GetDistance(sourcePos, targetPos) > moveSpeed)
            {
                var xdif = targetPos.x - sourcePos.x;
                var ydif =targetPos.y - sourcePos.y;

                var total = Math.Abs(xdif) + Math.Abs(ydif);
                var perX = xdif / total;
                var perY = ydif / total;
                var newX = (int)(perX * moveSpeed);
                var newY = (int)(perY * moveSpeed);
                if (Math.Abs(newX + newY) == moveSpeed)
                {
                    return new Vector2(sourcePos.x + newX, sourcePos.y + newY);
                }
                if (Math.Abs(newX + newY) == moveSpeed -1)
                {
                    return new Vector2(sourcePos.x + newX, sourcePos.y + newY - 1);
                }
                if (Math.Abs(newX + newY) == moveSpeed +1)
                {
                    return new Vector2(sourcePos.x + newX, sourcePos.y + newY + 1);
                }
                return new Vector2(sourcePos.x + newX, sourcePos.y + newY);
            }
            else
            {
                //If where you want to go is within your movespeed feel free man
                return targetPos;
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
