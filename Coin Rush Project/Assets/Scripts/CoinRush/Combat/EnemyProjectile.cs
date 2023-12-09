using System;
using CoinRush.Attributes;
using UnityEngine;

namespace CoinRush.Combat
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float _projectileDamage;
    
        public float speed = .1f;
        public float rotationSpeed = 200f;
        
        private float _destroyTime = 5f;
        private Transform _target;
    
        // Set the target enemy for the projectile
        public void SetTarget(GameObject newTarget)
        {
            _target = newTarget.transform;
        }
    
        void Start()
        {
            if (_target == null)
            {
                Destroy(gameObject); // Destroy the projectile if the target is lost
                return;
            }

            // Calculate the direction to the target
            Vector3 direction = _target.position - transform.position;
            direction.Normalize();

            // Calculate the rotation step
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Calculate the new direction after rotation
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationStep, 0.0f);

            // Rotate towards the new direction
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Move the projectile forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void Update()
        {
            Destroy(gameObject, _destroyTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                other.gameObject.GetComponent<Health>().TakeDamage(_projectileDamage);
                
                Debug.Log("Player hit");
            
                Destroy(gameObject);
            }
        }
    }
}
