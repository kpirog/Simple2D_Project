using UnityEngine;

[CreateAssetMenu(fileName = "NewRoundData", menuName = "Rounds/New Round", order = 0)]
public class RoundData : ScriptableObject
{
    [Header("Spawning settings")]
    [SerializeField][Range(1, 4)] private int activeSpawnersCount;
    [SerializeField] private float timeToFirstSpawn;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnDelay;

    [Header("Round Goal settings")]
    [SerializeField] private int mushroomsToKill;
    [SerializeField] private int goblinsToKill;
    [SerializeField] private int goldToCollect;

    public int ActiveSpawnersCount => activeSpawnersCount;
    public float TimeToFirstSpawn => timeToFirstSpawn;
    public float SpawnRate => spawnRate;
    public float SpawnDelay => spawnDelay;
    public int MushroomsToKill => mushroomsToKill;
    public int GoblinsToKill => goblinsToKill;
    public int GoldToCollect => goldToCollect;  

    private void OnValidate()
    {
        if (timeToFirstSpawn <= 0)
            timeToFirstSpawn = 0.1f;

        if (spawnRate <= 0)
            spawnRate = 1f;

        if (spawnDelay < 1)
            spawnDelay = 1;
    }
}
