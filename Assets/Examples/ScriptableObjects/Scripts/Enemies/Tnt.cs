using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.ScriptableObjects
{
    public class Tnt : Enemy
    {
        public float explosionRadius = 5f;
        public float minDistance = 1f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, minDistance * 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }

        public void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            for (int i = 0; i < hits.Length; i++)
            {
                Player player = hits[i].GetComponent<Player>();
                if(player)
                { 
                player.health -= damage;
                }
                Enemy enemy = hits[i].GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.health -= damage;
                }
            }
            Destroy(gameObject);
        }

        public override void Death()
        {
            Explode();
        }

        public override void Attack()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= minDistance)
            {
                Explode();
            }
        }
    }
}