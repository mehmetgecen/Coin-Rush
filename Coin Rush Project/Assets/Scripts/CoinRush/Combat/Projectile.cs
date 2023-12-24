
using CoinRush.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace CoinRush.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private UnityEvent onProjectileHit;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private bool isHoming;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private float maxLifetime = 5f;
        [SerializeField] private float projectileDamage;
        Health _target = null;

        private void Start()
        {
            transform.LookAt(GetAimPosition());    
        }
    
        void Update()
        {
            if (isHoming && !_target.IsDead())
            {
                transform.LookAt(GetAimPosition()); 
            }
            
            //TODO tween projectile movement
            
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    
        public void SetTarget(Health target)
        {
            _target = target;
            Destroy(gameObject,maxLifetime);
        }
        
        private Vector3 GetAimPosition()
        {
            CapsuleCollider targetCapsule = _target.GetComponent<CapsuleCollider>();
    
            if (targetCapsule == null)
            {
                return _target.gameObject.transform.position;
            }
            return _target.gameObject.transform.position + (Vector3.up * targetCapsule.height / 2);
        }
        
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target) return;
            if(_target.IsDead()) return;
            
            _target.TakeDamage(projectileDamage);
            projectileSpeed = 0;
            
            onProjectileHit.Invoke();
            
            Destroy(gameObject,.25f);
            
            if (gameObject.name.Contains("Fireball")) // will be edited for performance issues
            {
                Instantiate(hitEffect, GetAimPosition(), Quaternion.identity);
                
            }
            
        }
        
    }

}
