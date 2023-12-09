using System;
using CoinRush.Attributes;
using CoinRush.Combat;
using UnityEngine;

namespace CoinRush.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private float cooldownTime = 2f;
        [SerializeField] private Transform _handTransform;
        [SerializeField] private GameObject projectilePrefab;
        
        public LayerMask enemyLayer;
        public float projectileSpeed = 10f;
        
        public bool isOnCooldown = false;
        
        private void Update()
        {
            Vector3 playerPosition = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPosition, detectionRadius, enemyLayer);

            if (hitColliders.Length > 0)
            {
                GameObject nearestEnemy = FindNearestEnemy(playerPosition, hitColliders);
                if (nearestEnemy != null && !nearestEnemy.GetComponent<Health>().IsDead() && !isOnCooldown)
                {
                    ThrowProjectile(nearestEnemy);
                }
            }
        }

        public GameObject FindNearestEnemy(Vector3 playerPosition, Collider[] enemies)
        {
            GameObject nearestEnemy = null;
            float nearestDistance = float.MaxValue;

            foreach (var enemyCollider in enemies)
            {
                float distance = Vector3.Distance(playerPosition, enemyCollider.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemyCollider.gameObject;
                }
            }

            return nearestEnemy;
        }
        
        void ThrowProjectile(GameObject enemy)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, _handTransform.position, transform.rotation);
            
            HomingProjectile homingProjectile = projectileObject.GetComponent<HomingProjectile>();
            
            homingProjectile.SetTarget(enemy);
            
            Rigidbody projectileRb = projectileObject.GetComponent<Rigidbody>();
            
            float throwForce =2f;
            projectileRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            
            isOnCooldown = true;
            Invoke("ResetCooldown", cooldownTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                gameObject.GetComponent<Health>().Die();
                Debug.Log("Player died instantly");
            }
        }

        void ResetCooldown()
        {
            isOnCooldown = false;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        
    }
    
    
        
        
}

