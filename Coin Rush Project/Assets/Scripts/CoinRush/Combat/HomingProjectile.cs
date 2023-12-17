using System;
using CoinRush.Attributes;
using UnityEngine;

namespace CoinRush.Combat
{
    public class HomingProjectile : MonoBehaviour
    {
        public float speed = .1f;
        public float rotationSpeed = 200f;
        
        private float _destroyTime = 5f;
        private Transform _target;

        // Set the target enemy for the projectile
        public void SetTarget(GameObject newTarget)
        {
            _target = newTarget.transform;
        }

        void Update()
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

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy")) // Adjust the tag based on your enemy GameObjects
            {
                other.gameObject.GetComponent<Health>().Die();
                
                Debug.Log("enemy died");
                
                // Handle collision with the target (e.g., deal damage)
                Destroy(gameObject);
            }
        }
    }
}
