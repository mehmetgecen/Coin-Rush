using System;
using CoinRush.Attributes;
using CoinRush.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace CoinRush.Control
{
    public class EnemyController : MonoBehaviour
    {
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private Health _health;
        private Health _playerHealth;
        private Health _target;

        
        #region Attack Attributes
        
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private float cooldownTime = 2f;
        
        [SerializeField] private Projectile projectile = null;
        [SerializeField] private GameObject _rightHand;
        [SerializeField] private GameObject projectilePrefab;
        
        public bool isOnCooldown = false;
        
        private float lastAttackTime;

        #endregion
        
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _playerHealth = _player.GetComponent<Health>();
        }


        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();
            
            if(_health.IsDead()) return;
            
            // Check if the player is within the attack range
            
            if (DistanceToPlayer() <= attackRange)
            {
                if (_player == null || _player.GetComponent<Health>().IsDead() || isOnCooldown) return;
                SwitchToThrowAnimations();
                Throw();
                    
                Debug.Log("throw called from update.");

            }
            else
            {
                MoveTowardsPlayer();
            }
            
        }
        
        void MoveTowardsPlayer()
        {
            // Calculate the direction to the player
            var position = _player.transform.position;
            
            Vector3 direction = (position - transform.position).normalized;

            // Move towards the player
            _navMeshAgent.SetDestination(position);
            
            SwitchToRunAnimations();
        }
        
        
        private void Throw()
        {
            if (_target == null) return;
            
            transform.LookAt(_player.transform);
            
            // Stop moving
            _navMeshAgent.SetDestination(transform.position);
            
            LaunchProjectile(_rightHand.transform,_playerHealth);
            
            ThrowProjectile(_player);
            
            Debug.Log("projectile launched by enemy.");
            
            isOnCooldown = true;
            Invoke("ResetCooldown", cooldownTime);
            
            
            
        }

        public void LaunchProjectile(Transform rightHand,Health target)
        {
            if (projectile!=null)
            {
                Projectile projectileInstance = Instantiate(projectile,_rightHand.transform.position,Quaternion.identity);
                projectileInstance.SetTarget(target);
                
            }
            
        }
        
        void ThrowProjectile(GameObject enemy)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, _rightHand.transform.position, transform.rotation);
            
            projectile = projectileObject.GetComponent<Projectile>();
            
            //homingProjectile.SetTarget(enemy);
            
            Rigidbody projectileRb = projectileObject.GetComponent<Rigidbody>();
            
            float throwForce =2f;
            projectileRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            
            
        }
        
        void ResetCooldown()
        {
            isOnCooldown = false;
        }

        private void SwitchToThrowAnimations()
        {
            _animator.ResetTrigger("Run");
            _animator.SetTrigger("Throw");
        }
        
        private void SwitchToRunAnimations()
        {
            _animator.ResetTrigger("Throw");
            _animator.SetTrigger("Run");
        }
        
        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, _player.transform.position);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
