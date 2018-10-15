using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.ScriptableObjects
{
    public class Player : MonoBehaviour
    {
        public int health = 100;
        public float movementSpeed = 20f;
        public CharacterController controller;
        
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Vector3 force = new Vector3(inputH, 0, inputV);
            force *= movementSpeed * Time.deltaTime;
            controller.Move(force);

            if (health <= 0)
            {
                // You die!
                Death();
            }
        }

        void Death()
        {
            Destroy(gameObject);
        }
    }
}