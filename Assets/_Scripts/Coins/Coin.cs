using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;

    private ObjectPool<Coin> coinsPool;

    private void OnEnable()
    {
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
        if (collision.CompareTag("Player"))
        {
            AudioSystem.PlaySFX_Global(coinClip);
            
            EventManager.OnCoinCollected();

            coinsPool.Release(this);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            coinsPool.Release(this);
        }
    }
}
