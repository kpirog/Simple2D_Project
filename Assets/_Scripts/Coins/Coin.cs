using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    private CoinController coinController;
    private IObjectPool<Coin> coinsPool;

    private void Awake()
    {
        coinController = FindObjectOfType<CoinController>();
    }
    public void SetPool(IObjectPool<Coin> pool)
    {
        coinsPool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinController.onCoinCollected?.Invoke();
            coinsPool.Release(this);
        }
        else if(collision.CompareTag("Obstacle"))
        {
            coinsPool.Release(this);
        }
    }
}
