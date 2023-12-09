using CoinRush.Attributes;
using TMPro;
using UnityEngine;

namespace CoinRush.UI
{
    public class KillCountDisplayUI : MonoBehaviour
    {
        private Health _killCounter;
        
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = Health._killCount.ToString();
        }
    }
}
