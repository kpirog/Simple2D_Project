using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Coin coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private Vector2 spawnBoundaryX;
    [SerializeField] private Vector2 spawnBoundaryY;
    [SerializeField] private int maxCoinsAmount;
    [SerializeField] private float coinSpawnRate;
    [SerializeField] private float overlapCircleRadius;

    private ObjectPool<Coin> coinsPool;
    public Collider2D[] obstaclesColliders;
    private bool isActivated = true;

    private bool CanSpawnCoins => coinsPool.CountActive < maxCoinsAmount;

    [HideInInspector] public UnityEvent onCoinCollected;

    private void Awake()
    {
        coinsPool = new ObjectPool<Coin>(CreateCoin, OnGetCoin, OnReleaseCoin);
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
    private void Update()
    {
        if (CanSpawnCoins && !isActivated)
        {
            EnableSpawner();
        }
        else if (!CanSpawnCoins && isActivated)
        {
            DisableSpawner();
        }
    }
    private void OnGetCoin(Coin coin)
    {
        Vector2 randomPosition = SetRandomPositionInArea();

        coin.transform.position = randomPosition;
        coin.gameObject.SetActive(true);
    }
    private void OnReleaseCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }
    private void SpawnCoin()
    {
        coinsPool.Get();
    }
    private Vector2 SetRandomPositionInArea()
    {
        Vector2 newSpawnPosition = Vector2.zero;
        bool canSpawnHere = false;

        while (!canSpawnHere)
        {
            float posX = Random.Range(spawnBoundaryX.x, spawnBoundaryX.y);
            float posY = Random.Range(spawnBoundaryY.x, spawnBoundaryY.y);

            newSpawnPosition = new Vector2(posX, posY);
            canSpawnHere = PreventSpawnOverlap(newSpawnPosition);

            if (canSpawnHere) { break; }
        }

        return newSpawnPosition;
    }
    private void EnableSpawner()
    {
        isActivated = true;
        InvokeRepeating(nameof(SpawnCoin), coinSpawnRate, coinSpawnRate);
    }
    private void DisableSpawner()
    {
        isActivated = false;
        CancelInvoke();
    }
    private bool PreventSpawnOverlap(Vector3 spawnPos)
    {
        obstaclesColliders = Physics2D.OverlapCircleAll(transform.position, overlapCircleRadius);

        for (int i = 0; i < obstaclesColliders.Length; i++)
        {
            Vector3 centerPoint = obstaclesColliders[i].bounds.center;

            float width = obstaclesColliders[i].bounds.extents.x;
            float height = obstaclesColliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float upperExtent = centerPoint.y + height;
            float lowerExtent = centerPoint.y - height;

            if ((spawnPos.x <= leftExtent && spawnPos.x >= rightExtent) || (spawnPos.y <= lowerExtent && spawnPos.x >= upperExtent))
            {
                return false;
            }
        }

        return true;
    }
}
