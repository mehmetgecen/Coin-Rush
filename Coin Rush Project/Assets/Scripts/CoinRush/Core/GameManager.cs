using CoinRush.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoinRush.Core
{
    public class GameManager : MonoBehaviour
    {
        public GameObject player;
        public Button restartButton;
        public TextMeshProUGUI gameOverText;
        
        private Health _playerHealth;

        private void Start()
        {
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
                GameOver();
            }
        }

        private void GameOver()
        {
            EnableRestartUI();
            Time.timeScale = 0f;
        }
        
        public void RestartGame()
        {
            // Reload the current scene to restart the game
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f; // Resume the game
            
            DisableRestartUI();
        }
        
        private bool PlayerIsDead()
        {
            return _playerHealth._isPlayerDead;
        }
        
        
    }
}
