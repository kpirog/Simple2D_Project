using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine[] enemyPrefabs;

    [Header("Spawning settings")]
    [SerializeField] private float timeToFirstSpawn;
    [SerializeField] private float spawnRate;

    private ObjectPool<EnemyStateMachine> enemiesPool;

    private void Awake()
    {
        enemiesPool = new ObjectPool<EnemyStateMachine>(CreateEnemy, OnGetEnemy, OnReleaseEnemy);
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), timeToFirstSpawn, spawnRate);
    }
    private EnemyStateMachine CreateEnemy()
    {
        EnemyStateMachine enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position, Quaternion.identity);
        enemy.SetPool(enemiesPool);

        return enemy;
    }
    private void SpawnEnemy()
    {
        enemiesPool.Get();
    }
    private void OnGetEnemy(EnemyStateMachine enemy)
    {
        enemy.enemyHealth.ReviveEnemy();
        enemy.SwitchState(new EnemyIdleState(enemy));
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }
    private void OnReleaseEnemy(EnemyStateMachine enemy)
    {
        enemy.anim.Rebind();
        enemy.gameObject.SetActive(false);
    }
}
