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
        private Health _health;
        private Health _playerHealth;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;


        #region Attack Attributes
        
        [SerializeField] private float _attackCooldown = 5f;
        [SerializeField] private float attackRange = 10f;
        
        private float projectileDamage = 10f;
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
            
            // Check if the player is within the attack range
            if(_health.IsDead()) return;
            
            if (DistanceToPlayer() <= attackRange)
            {
                if (Time.time - lastAttackTime >= _attackCooldown)
                {
                    Throw();
                    lastAttackTime = Time.time;
                }
                else
                {
                    _animator.ResetTrigger("Run");
                    _animator.ResetTrigger("Throw");
                }
                
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
        
        
        void Throw()
        {
            transform.LookAt(_player.transform);
            
            // Stop moving
            _navMeshAgent.SetDestination(transform.position);
            
            SwitchToThrowAnimations();
            _playerHealth.TakeDamage(projectileDamage);
            
        }

        private void SwitchToThrowAnimations()
        {
            //_animator.ResetTrigger("Run");
            _animator.SetTrigger("Throw");
        }
        
        private void SwitchToRunAnimations()
        {
            //_animator.ResetTrigger("Throw");
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
