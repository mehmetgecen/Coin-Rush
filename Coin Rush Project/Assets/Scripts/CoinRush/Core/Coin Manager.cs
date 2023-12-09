using System.Collections.Generic;
using UnityEngine;

namespace CoinRush.Core
{
    public class CoinManager : MonoBehaviour
    {
        public GameObject coinPrefab;
        public int poolSize = 10;

        private List<GameObject> coinPool;

        void Start()
        {
            InitializePool();
            SpawnCoins();
        }

        void InitializePool()
        {
            coinPool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject coin = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity);
                coin.SetActive(false);
                coinPool.Add(coin);
            }
        }

        void SpawnCoins()
        {
            for (int i = 0; i < poolSize; i++)
            {
                SpawnCoin();
            }
        }

        void SpawnCoin()
        {
            GameObject coin = GetPooledCoin();
            if (coin != null)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
                coin.transform.position = randomPosition;
                coin.SetActive(true);
            }
        }

        GameObject GetPooledCoin()
        {
            foreach (GameObject coin in coinPool)
            {
                if (!coin.activeInHierarchy)
                {
                    return coin;
                }
            }

            // If all coins are in use, you could optionally expand the pool here.
            return null;
        }
    }
}
