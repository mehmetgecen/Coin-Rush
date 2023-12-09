using System.Collections;
using System.Collections.Generic;
using CoinRush.Core;
using TMPro;
using UnityEngine;

public class CoinDisplayUI : MonoBehaviour
{
    
    private PickUp coinPickUp;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PickUp._collectedCoinCount.ToString();
    }
}
