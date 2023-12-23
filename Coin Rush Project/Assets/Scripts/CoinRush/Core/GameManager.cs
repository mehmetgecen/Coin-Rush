using System.Collections;
using System.Collections.Generic;
using CoinRush.Attributes;
using CoinRush.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoinRush.Core
{
    public class GameManager : MonoBehaviour
    {
        public PlayerData playerData;
        public GameObject player;
        public Button restartButton;
        public TextMeshProUGUI gameOverText;
        public GameObject healthBar;
        
        private Health _playerHealth;

        private void Start()
        {
            if (playerData == null)
            {
                playerData = FindObjectOfType<PlayerData>();
            }

            // Load player data when the game starts
            playerData.LoadPlayerData();
            
            Time.timeScale = 1f;
            _playerHealth = player.GetComponent<Health>();
        }

        private void EnableRestartUI()
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        
        private void DisableRestartUI()
        {
            restartButton.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (PlayerIsDead())
            {
                StartCoroutine(RestartUIWait());
            }
        }

        private void GameOver()
        {
            playerData.SavePlayerData();
            EnableRestartUI();
            Time.timeScale = 0f;
            
        }
        
        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f; // Resume the game
            
            DisableRestartUI();
        }

        private IEnumerator RestartUIWait()
        {
            DisableHealthBar();
            yield return new WaitForSeconds(3f);
            GameOver();
            
        }
        
        private void DisableHealthBar()
        {
            healthBar.SetActive(false);
        }
        
        private bool PlayerIsDead()
        {
            return _playerHealth._isPlayerDead;
        }
        
        
    }
}
