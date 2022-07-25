using UnityEngine;
using UnityEngine.Pool;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine[] enemyPrefabs;

    [Header("Spawning settings")]
    [SerializeField] private float timeToFirstSpawn;
    [SerializeField] private float spawnRate;

    private ObjectPool<EnemyStateMachine> enemiesPool;
    private EnemyStateMachine lastSpawnedEnemy;

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
        EnemyStateMachine selectedEnemy = SelectEnemyToSpawn(ref lastSpawnedEnemy);

        EnemyStateMachine enemy = Instantiate(selectedEnemy, transform.position, Quaternion.identity, transform);
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
    private EnemyStateMachine SelectEnemyToSpawn(ref EnemyStateMachine enemy)
    {
        if (enemy == null)
        {
            enemy = GetRandomEnemy();
        }
        else
        {
            EnemyType lastEnemyType = enemy.EnemyType;
            enemy = enemyPrefabs.Where(x => x.EnemyType != lastEnemyType).FirstOrDefault();
        }

        return enemy;
    }
    private EnemyStateMachine GetRandomEnemy()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }
}
