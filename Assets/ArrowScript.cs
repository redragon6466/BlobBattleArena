using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class ArrowScript : MonoBehaviour
    {
        protected BlobScript creator;
        protected BlobScript target;
        protected BaseAttack sourceAttack;
        protected int speed;
        protected int state = 1;
        protected int timeToLive = 180;
        // Start is called before the first frame update
        void Start()
        {
            //state = 1;
            //Debug.Log("Arrow Created");

        }
        /**
         * States 
         * 1 - Just created, no values yet.
         * 2 - Target and parent set
         * 3 - Destroy it and clean up
         */
        public bool setState(int stateValue)
        {
            Debug.Log("State set: " + stateValue);
            state = stateValue;
            return true;
        }
        public void setTargetAndParent(BlobScript parent, BlobScript enemy, BaseAttack source)
        {
            creator = parent;
            target = enemy;
            sourceAttack = source;


            setState(2);
        }


        // Update is called once per frame
        void Update()
        {
            //Debug.Log(state);
            switch (state)
            {
                case 1:
                    return;
                case 2:
                    MoveToTarget();
                    break;
                case 3:
                    timeToLive--;
                    GetComponent<Rigidbody2D>().velocity = transform.up * 200 * Time.deltaTime;
                    if (timeToLive <= 0 || target == null || creator == null || (Math.Abs(target.transform.position.x - gameObject.transform.position.x) < 1 && Math.Abs(target.transform.position.y - gameObject.transform.position.y) < 1))
                    {
                        setState(4);
                    }
                    break;
                case 4:
                    FindObjectOfType<God>().EndTurn();
                    CleanUp();
                    break;
                default:
                    Debug.Log("Arrow Script, Bad State Value");
                    break;
            }
        }


        private void MoveToTarget()
        {
            setRotation();
            setState(3);
        }
        public void setRotation()
        {
            float TargetLocX;
            float TargetLocY;
            float AttackerLocX;
            float AttackerLocY;

            if (target == null || creator == null)
            {
                setState(4);
                return;
            }



            //TargetLocX = target.gameObject.transform.position.x;
            // TargetLocY = target.gameObject.transform.position.y;
            //AttackerLocX = creator.gameObject.transform.position.x;
            //AttackerLocY = creator.gameObject.transform.position.y;


            //float opp = TargetLocY - AttackerLocY;
            //float hyp = (float)Math.Sqrt(Math.Pow(TargetLocY - AttackerLocX,(float)2) + Math.Pow(TargetLocX - AttackerLocX,(float)2));
            // float sine = opp / hyp;
            //Debug.Log(sine);



            Vector3 targetDir = target.transform.position - creator.transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);

            if (creator.transform.position.x < target.transform.position.x)
            {
                angle = angle * -1;
            }


            //this.transform.Rotate(0, 0, sine * (float)(180/Math.PI), Space.World);
            this.transform.Rotate(0, 0, angle, Space.World);



        }


        private void CleanUp()
        {
            Destroy(this.gameObject);
        }
    }

}

