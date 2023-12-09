using CoinRush.Core;
using TMPro;
using UnityEngine;

namespace CoinRush.UI
{
    public class CoinDisplayUI : MonoBehaviour
    {
        private PickUp coinPickUp;

        // Update is called once per frame
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = PickUp._collectedCoinCount.ToString();
        }
    }
}
