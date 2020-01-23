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

        public virtual void TakeTurn(GameObject me, GameObject[] allyBlobs, GameObject[] enemyBlobs)
        {
            
        }

        /// <summary>
        /// Get the length of it, L, and multiply all components by M/L, where M is the new length of the vector.
        /// </summary>
        /// <param name="myPos"></param>
        /// <param name="otherPos"></param>
        /// <param name="moveSpeed"></param>
        /// <returns></returns>
        public Vector3 MoveTo(Vector3 myPos, Vector3 otherPos, float moveSpeed)
        {

            Debug.Log((myPos - otherPos).magnitude);
            if ((myPos - otherPos).magnitude > moveSpeed)
            {
                var temp1 = myPos.magnitude + moveSpeed;
                Debug.Log("MS: " + temp1);
                Debug.Log(string.Format("{0}, {1}, {2}", myPos.x * temp1 / myPos.magnitude, myPos.y * temp1 / myPos.magnitude, myPos.z * temp1 / myPos.magnitude));
                return new Vector3(myPos.x * temp1 / myPos.magnitude, myPos.y * temp1 / myPos.magnitude, myPos.z * temp1 / myPos.magnitude);
            }
            else
            {
                return otherPos;
            }


        }

        public GameObject ClostedBlob(GameObject myPos, List<GameObject> otherPositions)
        {
            var distances = DistancesToMulti(myPos, otherPositions);

            return distances.FirstOrDefault().Item2;
        }

        public GameObject FarthestBlob(GameObject myPos, List<GameObject> otherPositions)
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
        public List<Tuple<float, GameObject>> DistancesToMulti(GameObject myPos, List<GameObject> otherPositions)
        {
            List<Tuple<float, GameObject>> distances = new List<Tuple<float, GameObject>>();

            foreach (var pos in otherPositions)
            {
                distances.Add(new Tuple<float, GameObject>(Vector3.Distance(myPos.transform.position, pos.transform.position), pos));
            }


            return distances.OrderBy(x => x.Item1).ToList();
        }

        public float CalculateDistance(Vector3 myPos, Vector3 otherPos)
        {
            return Mathf.Pow(Mathf.Pow(otherPos.x - myPos.x, 2f) + Mathf.Pow(otherPos.y - myPos.y, 2f) + Mathf.Pow(otherPos.z - myPos.z, 2f), .5f);
        }
    }
}
