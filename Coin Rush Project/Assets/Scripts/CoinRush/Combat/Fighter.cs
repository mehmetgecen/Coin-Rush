using CoinRush.Attributes;
using UnityEngine;

namespace CoinRush.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] public float attackRange = 10f;

        public bool isOnCooldown = false;
        
        private Health _target;
        private Projectile _projectile;
        private float _timeSinceLastAttack = Mathf.Infinity;
        
        /*
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target == null) return;
            if (_target.IsDead()) return;
            
            if (CanAttack(_target.gameObject))
            {
                AttackBehaviour();
                FireProjectile(_target.gameObject);
                    
                Debug.Log("Fighter Attack Behaviour Called.");
            }
            
        }*/
        public void FireProjectile(GameObject target)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rightHand.transform.position, transform.rotation);
            _projectile = projectileObject.GetComponent<Projectile>();

            if (gameObject.CompareTag("Enemy"))
            {
                target = GameObject.FindWithTag("Player");
            }
            
            _projectile.SetTarget(target.GetComponent<Health>());
            
            /*
            Rigidbody projectileRb = projectileObject.GetComponent<Rigidbody>();
            float throwForce =2f;
            projectileRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);*/
            
            isOnCooldown = true;
            Invoke(nameof(ResetCooldown), attackCooldown);
            
        }
        
        // find the nearest enemy

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health combatTargetHealth = combatTarget.GetComponent<Health>();
            
            return combatTargetHealth != null && !combatTargetHealth.IsDead();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            
            if (_timeSinceLastAttack >= attackCooldown)
            {
                _timeSinceLastAttack = 0;
                TriggerAttackAnimations();
                print("Attack Behaviour Called.");
            }
        }
        
        void ResetCooldown()
        {
            isOnCooldown = false;
        }
        
        
        //TODO animation triggers must re-edited.
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
        
        
    }
}
