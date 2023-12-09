using System.Collections;
using CoinRush.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CoinRush.Attributes
{
    public class Health : MonoBehaviour
    {
        //TODO unity events and die animations will be set.
        
        [SerializeField] private UnityEvent<float> damageTaken;
        [SerializeField] private UnityEvent onDie;
        
        [SerializeField] private float startHealth = 100f;
        [SerializeField] private float health;
        //[SerializeField] private bool _hasHealthBar;

        public HealthBarDisplay _healthBar;
        
        private bool _isDead = false;
        public bool _isPlayerDead = false;
        
        public static int _killCount = 0;
        
        
        
        private void Start()
        {
            /*if (health<0)
            {
                //health = PlayerPrefs.GetFloat("Health",startHealth);    
            }*/
            
            health = startHealth;

            if (_healthBar!=null)
            {
                _healthBar.SetMaxHealth(startHealth);
            }
            
        }
        
        public void TakeDamage(float damage)
        {
            //print(gameObject.name + " took damage: " + damage);
            
            health = Mathf.Max(health - damage, 0);
            
            if (health==0)
            {
                onDie.Invoke();
                Die();
                
                

                
            }

            else
            {
                damageTaken.Invoke(damage);
            }

            if (_healthBar!=null)
            {
                _healthBar.SetHealth(health);
            }
            
        }
        
        public void Die()
        {
            if (_isDead) return;
            
            _isDead = true;
            
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
            Debug.Log("from health component: " + gameObject.name + " died.");
            
            if (gameObject.CompareTag("Enemy"))
            {
                _killCount++;
                GetComponent<Animator>().SetTrigger("Die");
            }
            
            if (gameObject.CompareTag("Player"))
            {
                _isPlayerDead = true;
                GetComponent<Animator>().SetTrigger("Die");
            }
            
        }
        public bool IsDamageTaken()
        {
            return health < startHealth;
        }
        
        public bool IsDead()
        {
            return _isDead;
        }
        
        public float GetHealthPoints()
        {
            return health;
        }
    }
}
