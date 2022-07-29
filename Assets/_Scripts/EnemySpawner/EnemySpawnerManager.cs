using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> spawners;

    private float spawnDelay;

    public void SetRoundData(RoundData roundData)
    {
        for (int i = 0; i < roundData.ActiveSpawnersCount; i++)
        {
            if (i == 0) { spawnDelay = 0f; }
            else { spawnDelay += roundData.SpawnDelay; }

            spawners[i].IsActive = true;
            spawners[i].SetRoundDataSettings(roundData.TimeToFirstSpawn + spawnDelay, roundData.SpawnRate + spawnDelay);
        }
    }
}
