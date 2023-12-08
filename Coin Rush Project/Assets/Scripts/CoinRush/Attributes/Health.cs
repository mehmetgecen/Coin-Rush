using UnityEngine;
using UnityEngine.Events;

namespace CoinRush.Attributes
{
    public class Health : MonoBehaviour
    {
        //TODO unity events and die animations will be set.
        
        [SerializeField] private UnityEvent<float> damageTaken;
        [SerializeField] private UnityEvent onDie;
        
        [SerializeField] private float startHealth;
        [SerializeField] private float health = 100f;
        
        private bool _isDead = false;
        public bool _isPlayerDead = false;
        
        
        
        private void Start()
        {
            /*if (health<0)
            {
                //health = PlayerPrefs.GetFloat("Health",startHealth);    
            }*/
            
            //health = startHealth;
        }
        
        public void TakeDamage(float damage)
        {
            //print(gameObject.name + " took damage: " + damage);
            
            health = Mathf.Max(health - damage, 0);
            
            if (health==0)
            {
                onDie.Invoke();
                Die();
                
                if (gameObject.CompareTag("Player"))
                {
                    _isPlayerDead = true;
                }
            }

            else
            {
                damageTaken.Invoke(damage);
            }
        }
        
        private void Die()
        {
            if (_isDead) return;
            
            _isDead = true;
            
            GetComponent<Animator>().SetTrigger("Die");

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
