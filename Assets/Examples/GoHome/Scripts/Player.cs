using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class Player : MonoBehaviour
    {
        public float speed = 20f;
        public float maxVelocity = 20f;
        public Rigidbody rigid;

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