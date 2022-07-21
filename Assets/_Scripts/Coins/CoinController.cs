using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    private List<Coin> coinsList;

    [HideInInspector] public UnityAction<int> onCoinCollected;

    private void Awake()
    {
        coinsList = new List<Coin>(FindObjectsOfType<Coin>());
    }
}
