using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinController coinController;

    private void Awake()
    {
        coinController = FindObjectOfType<CoinController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinController.onCoinCollected?.Invoke(1);
            gameObject.SetActive(false);
        }
    }
}
