using Assets.Services.Containers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services
{
    public class AnimationService : IAnimationService
    {
        private bool areWeDone;
        private static AnimationService instance = null;
        
        private static readonly object padlock = new object();
        

        AnimationService()
        {
        }

        public static AnimationService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AnimationService();
                    }
                    return instance;
                }
            }
        }
        public void StepSquare(Vector2 startPoint, Vector2 endPoint, GameObject unit)
        {
            //areWeDone = false;
            var xdif = startPoint.x - endPoint.x;
            var ydif = startPoint.y - endPoint.y;
            Vector3 directionVect;
            if(xdif > 0)
            {
                if(ydif > 0)
                {
                    directionVect = Vector3.right;
                }
                else
                {
                    directionVect = Vector3.right;
                }
            }
            else
            {
                if (ydif > 0)
                {
                    directionVect = Vector3.right;
                }
                else
                {
                    directionVect = Vector3.right;
                }
            }

            //Vector3 walkingDistance = (startPoint - endPoint);
            //Debug.Log("Start" + startPoint + "end " + endPoint + "walk" + walkingDistance);

            Walk(startPoint, endPoint, directionVect, unit);
        }
        private void Walk(Vector2 startPoint, Vector2 endPoint, Vector3 distanceWalked, GameObject walker)
        {
            //while I'm not at my destination
            //keep walking
            //if I am
            //stop walking, end turn.
            while(!startPoint.Equals(endPoint))
            {
                Debug.Log("START" + startPoint + "end" + endPoint);
                walker.transform.position += (Vector3.right * 5 * Time.deltaTime);
            }

            UnityEngine.Object.FindObjectOfType<God>().EndTurn();


        }

        public void WalkRoute(Vector2[] steps,GameObject unit)
        {

        }
        


    }
        
}

