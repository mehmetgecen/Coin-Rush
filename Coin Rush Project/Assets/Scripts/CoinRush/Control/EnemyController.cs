using System;
using CoinRush.Attributes;
using CoinRush.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace CoinRush.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour
    {
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private Fighter _fighter;
        
        private Health _health;
        private Health _playerHealth;
        private Health _target;
        
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _playerHealth = _player.GetComponent<Health>();
            
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();
            
            if(_health.IsDead()) return;
            
            // Check if the player is within the attack range
            
            if (DistanceToPlayer() <= _fighter.attackRange)
            {
                if (_player == null || _playerHealth.IsDead() || _fighter.isOnCooldown) return;
                
                //_fighter.FireProjectile(_player);
                SwitchToThrowAnimations();
                    
                Debug.Log("Throw called from Enemy Update.");

            }
            else
            {
                MoveTowardsPlayer();
            }
            
        }

        private void MoveTowardsPlayer()
        {
            // Calculate the direction to the player
            var position = _player.transform.position;
            
            //Vector3 direction = (position - transform.position).normalized;

            // Move towards the player
            _navMeshAgent.SetDestination(position);
            
            SwitchToRunAnimations();
        }
        
        
        /*private void Throw()
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
             
        }*/
        
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
            Gizmos.DrawWireSphere(transform.position, _fighter.attackRange);
        }
    }
}
