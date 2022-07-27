using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    private ObjectPool<Coin> coinsPool;

    private bool isCollected;

    private void OnEnable()
    {
        isCollected = false;
        EventManager.OnRoundStart += EventManager_OnRoundStart;
    }
    private void OnDisable()
    {
        EventManager.OnRoundStart -= EventManager_OnRoundStart;
    }
    private void EventManager_OnRoundStart()
    {
        coinsPool.Release(this);
    }
    public void SetPool(ObjectPool<Coin> pool)
    {
        coinsPool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            EventManager.OnCoinCollected();

            coinsPool.Release(this);

            isCollected = true;
        }
        else if (collision.CompareTag("Obstacle"))
        {
            coinsPool.Release(this);
        }
    }
}
