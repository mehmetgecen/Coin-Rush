using CoinRush.Attributes;
using CoinRush.Combat;
using UnityEngine;

namespace CoinRush.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 5f;
        //[SerializeField] private Projectile projectile = null;
        public LayerMask enemyLayer;
        public float projectileSpeed = 10f;
        
        [SerializeField] private Transform _handTransform;
        [SerializeField] private GameObject projectilePrefab;

        private void Update()
        {
            Vector3 playerPosition = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPosition, detectionRadius, enemyLayer);

            if (hitColliders.Length > 0)
            {
                GameObject nearestEnemy = FindNearestEnemy(playerPosition, hitColliders);
                if (nearestEnemy != null)
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
            // Instantiate the projectile
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
            
            HomingProjectile homingProjectile = projectileObject.GetComponent<HomingProjectile>();

            // Set the target enemy for the projectile
            homingProjectile.SetTarget(enemy);
            

            // Get the Rigidbody component of the projectile
            Rigidbody projectileRb = projectileObject.GetComponent<Rigidbody>();

            // Apply force to the projectile
            float throwForce =2f;
            projectileRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        
    }
    
    
        
        
}

