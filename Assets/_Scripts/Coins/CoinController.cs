using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float coinSpawnRate;
    [SerializeField] private int maxCoinAmount;

    private IObjectPool<Coin> coinsPool;
    
    [HideInInspector] public UnityEvent onCoinCollected;

    private void Awake()
    {
        coinsPool = new ObjectPool<Coin>(CreateCoin, OnGetCoin, OnReleaseCoin, OnDestroyCoin, maxSize: maxCoinAmount);
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), 0f, coinSpawnRate);
    }
    private Coin CreateCoin()
    {
        Coin coin = Instantiate(coinPrefab, Vector2.zero, Quaternion.identity, transform);
        coin.SetPool(coinsPool);

        return coin;
    }
    private void OnGetCoin(Coin coin)
    {
        coin.transform.position = SetRandomPositionInArea();
        coin.gameObject.SetActive(true);
    }
    private void OnReleaseCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }
    private void OnDestroyCoin(Coin coin)
    {
        Destroy(coin.gameObject);
    }
    private void SpawnCoin()
    {
        coinsPool.Get();
    }
    private Vector2 SetRandomPositionInArea()
    {
        float posX = Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x);
        float posY = Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y);

        return new Vector2(posX, posY);
    }
}
