using CoinRush.Attributes;
using UnityEngine;

namespace CoinRush.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private float attackRange = 10f;
        [SerializeField] private float _weaponDamage;
        
        private float _timeSinceLastAttack = Mathf.Infinity;
        private Health _target;
        

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target == null) return;
            if (_target.IsDead()) return;
            
            if (_target!=null)
            {
                if (IsInRange())
                {
                    AttackBehaviour();
                    
                    Debug.Log("Fighter AttackBehaviour Called.");
                }
                
            }
        }
        
        public Health GetTarget()
        {
            if (gameObject.CompareTag("Enemy"))
            {
                _target = GameObject.FindWithTag("Player").GetComponent<Health>();
            }

            if (gameObject.CompareTag("Player"))
            {
                FindNearestEnemy();
            }
            
            return _target;
        }
        
        // find the nearest enemy
        public void FindNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearestEnemy = null;
            float shortestDistance = Mathf.Infinity;
            Vector3 position = transform.position;
            
            foreach (GameObject enemy in enemies)
            {
                Vector3 direction = enemy.transform.position - position;
                float distance = direction.sqrMagnitude;
                
                if (distance < shortestDistance)
                {
                    nearestEnemy = enemy;
                    shortestDistance = distance;
                }
            }
            
            if (nearestEnemy != null && shortestDistance <= attackRange)
            {
                _target = nearestEnemy.GetComponent<Health>();
            }
        }
        
        
        
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health combatTargetHealth = combatTarget.GetComponent<Health>();
            
            return combatTargetHealth != null && !combatTargetHealth.IsDead();
        }
        
        public void Attack(GameObject combatTarget)
        {
            _target = combatTarget.GetComponent<Health>();
            _target.TakeDamage(_weaponDamage);
        }
        
        public void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            
            if (_timeSinceLastAttack >= attackCooldown)
            {
                _timeSinceLastAttack = 0;
                TriggerAttackAnimations();
                print("Attack Behaviour Called.");
            }
        }
        
        private void TriggerAttackAnimations()
        {
            if (gameObject.CompareTag("Enemy"))
            {
                GetComponent<Animator>().SetTrigger("Throw");
            }
            
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }
        
        private void StopAttackAnimatons()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("CancelAttack");
        }
        
        private bool IsInRange()
        {
            _target = GetTarget();
            
            return Vector3.Distance(_target.transform.position, transform.position) < attackRange;
        }
    }
}
