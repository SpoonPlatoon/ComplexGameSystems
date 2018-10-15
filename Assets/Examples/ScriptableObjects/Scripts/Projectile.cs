using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.ScriptableObjects
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 10f;
        public Vector3 direction;
        public Rigidbody rigid;

        void Update()
        {
            rigid.velocity += direction * speed;
        }
    }
}