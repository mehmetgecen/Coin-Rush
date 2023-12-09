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
                GameOver();
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
        
        public void ExitGame()
        {
            playerData.SavePlayerData();
            
            Debug.Log("Saving data and exiting the game...");

#if UNITY_EDITOR
            // If running in the Unity Editor, stop playing the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            If not in the Unity Editor, close the application
        Application.Quit();
#endif
        }
        
        private bool PlayerIsDead()
        {
            return _playerHealth._isPlayerDead;
        }
        
        
    }
}
