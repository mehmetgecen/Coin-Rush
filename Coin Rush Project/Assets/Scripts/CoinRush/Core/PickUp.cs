using System.Collections;
using UnityEngine;

namespace CoinRush.Core
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private float spawnTime = 5;

        public static int _collectedCoinCount = 0;
        
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Pickup(other.gameObject);
                _collectedCoinCount++;
                print(_collectedCoinCount);
            }
            
        }
        
        private void Pickup(GameObject subject)
        {
            StartCoroutine(SpawnPickup(spawnTime));
        }
        
        IEnumerator SpawnPickup(float seconds)
        {
            HidePickup();
            yield return new WaitForSeconds(seconds);
            ShowPickup();

        }

        private void ShowPickup()
        {
            GetComponent<SphereCollider>().enabled = true;
            Transform pickUpTransform = transform;
            
            for (int i = 0; i < pickUpTransform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void HidePickup()
        {
            GetComponent<SphereCollider>().enabled = false;

            Transform pickUpTransform = transform;
            
            for (int i = 0; i < pickUpTransform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
