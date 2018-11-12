using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GoHome
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 20f;
        public float maxVelocity = 20f;
        public Rigidbody rigid;

        private void Update()
        {
            Vector3 vel = rigid.velocity;
            if (Mathf.Abs(vel.x) > maxVelocity)
            {
                vel.x = vel.normalized.x * maxVelocity;
            }
            if (Mathf.Abs(vel.z) > maxVelocity)
            {
                vel.z = vel.normalized.z * maxVelocity;
            }
            rigid.velocity = vel;
        }
        //Constrict velocity per update
        //Collect item on trigger enter
        //Input method for movement
        public void Move(float inputH, float inputV)
        {
            //Generate direction from input (horizontal & vertical)
            Vector3 direction = new Vector3(inputH, 0, inputV);
            //Set velocity to direction (Not including y axis)
            Vector3 vel = rigid.velocity;
            vel.x = direction.x * speed;
            vel.z = direction.z * speed;
            //Apply velocity to rigidbody
            rigid.velocity = vel;
        }
    }
}