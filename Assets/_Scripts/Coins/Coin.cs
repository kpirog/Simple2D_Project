using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    private IObjectPool<Coin> coinsPool;

    private void OnEnable()
    {
        EventManager.OnRoundComplete += EventManager_OnRoundComplete;
    }
    private void OnDisable()
    {
        EventManager.OnRoundComplete -= EventManager_OnRoundComplete;
    }
    private void EventManager_OnRoundComplete(bool obj)
    {
        coinsPool.Release(this);
    }
    public void SetPool(IObjectPool<Coin> pool)
    {
        coinsPool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.OnCoinCollected();
            coinsPool.Release(this);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            coinsPool.Release(this);
        }
    }
}
