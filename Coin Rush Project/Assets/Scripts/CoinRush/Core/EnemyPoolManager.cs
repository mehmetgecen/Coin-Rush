using System.Collections.Generic;
using UnityEngine;

namespace CoinRush.Core
{
    public class EnemyPoolManager : MonoBehaviour
    {
        public Camera mainCamera;
        public GameObject enemyPrefab;
        public int poolSize = 10;

        private List<GameObject> enemyPool;

        void Start()
        {
            InitializePool();
            SpawnEnemies();
        }

        void InitializePool()
        {
            enemyPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                enemy.SetActive(false);
                enemyPool.Add(enemy);
            }
        }

        void SpawnEnemies()
        {
            for (int i = 0; i < poolSize; i++)
            {
                SpawnEnemy();
            }
        }

        void SpawnEnemy()
        {
            GameObject enemy = GetPooledEnemy();
            if (enemy != null)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
                enemy.transform.position = randomPosition;
                enemy.SetActive(true);
            }
        }

        GameObject GetPooledEnemy()
        {
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }

            // If all enemies are in use, you could optionally expand the pool here.
            return null;
        }
        
        Vector3 CalculateSpawnPosition()
        {
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            // You can adjust the spawn position based on your needs
            float spawnX = Random.Range(mainCamera.transform.position.x - cameraWidth / 2, mainCamera.transform.position.x + cameraWidth / 2);
            float spawnY = Random.Range(mainCamera.transform.position.y - cameraHeight / 2, mainCamera.transform.position.y + cameraHeight / 2 + 2f);

            return new Vector3(spawnX, spawnY, 0f);
        }
    }
}
