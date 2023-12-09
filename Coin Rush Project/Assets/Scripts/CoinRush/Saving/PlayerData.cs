using CoinRush.Attributes;
using CoinRush.Core;
using UnityEngine;

namespace CoinRush.Saving
{
    public class PlayerData : MonoBehaviour
    {
        
        private Health _killCounter;
        private PickUp coinCounter;

        // PlayerPrefs keys
        private string killCountKey = "TotalKillCount";
        private string coinsKey = "CollectedCoins";

        private void Start()
        {
            LoadPlayerData();
        }
        
        public void SavePlayerData()
        {
            PlayerPrefs.SetInt(killCountKey, Health._killCount);
            PlayerPrefs.SetInt(coinsKey, PickUp._collectedCoinCount);

            // Save the changes
            PlayerPrefs.Save();
        }
        
        public void LoadPlayerData()
        {
            
            if (PlayerPrefs.HasKey(killCountKey))
            {
                Health._killCount = PlayerPrefs.GetInt(killCountKey);
            }


            if (PlayerPrefs.HasKey(coinsKey))
            {
                PickUp._collectedCoinCount = PlayerPrefs.GetInt(coinsKey);
            }
        }


        public void UpdateKillCount(int amount)
        {
            Health._killCount += amount;
            SavePlayerData(); // Save the updated data
        }
        
        public void UpdateCollectedCoins(int amount)
        {
            PickUp._collectedCoinCount += amount;
            SavePlayerData(); // Save the updated data

        }
    }
}
