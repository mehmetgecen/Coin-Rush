using System;
using CoinRush.Attributes;
using CoinRush.Combat;
using DG.Tweening;
using UnityEngine;

namespace CoinRush.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public LayerMask enemyLayer;
        
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private GameObject _detectionZone;
        
        private Fighter _fighter;
        private Projectile _projectile;
        
        private Vector3 newScale;
        private float scaleFactor;
        
        private void Awake()
        {
            _fighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            AdjustDetectionZone();
        }
        
        private void Update()
        {
            Vector3 playerPosition = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPosition, detectionRadius, enemyLayer);

            if (hitColliders.Length > 0)
            {
                GameObject nearestEnemy = FindNearestEnemy(playerPosition, hitColliders);
                if (_fighter.CanAttack(nearestEnemy) && !_fighter.isOnCooldown)
                {
                    _fighter.FireProjectile(nearestEnemy);
                }
            }
        }

        private GameObject FindNearestEnemy(Vector3 playerPosition, Collider[] enemies)
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                gameObject.GetComponent<Health>().Die();
                Debug.Log("Player died instantly");
            }
        }
        
        private void AdjustDetectionZone()
        {
            scaleFactor = detectionRadius / 5;
            newScale = Vector3.one * scaleFactor;
            _detectionZone.transform.localScale = newScale;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        
    }
    
    
        
        
}

