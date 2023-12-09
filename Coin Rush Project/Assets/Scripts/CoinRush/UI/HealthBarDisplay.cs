using CoinRush.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CoinRush.UI
{
    public class HealthBarDisplay : MonoBehaviour
    {
        public Slider healthBarSlider;
        public Gradient gradient;
        public Image fill;
        
        private Health _health;
        
        public void SetMaxHealth(float health)
        {
            healthBarSlider.maxValue = health;
            healthBarSlider.value = health;

            fill.color = gradient.Evaluate(1f);
        }
        
        public void SetHealth(float health)
        {
            healthBarSlider.value = health;
            fill.color = gradient.Evaluate(healthBarSlider.normalizedValue);
        }
        
        
        
    }
}
