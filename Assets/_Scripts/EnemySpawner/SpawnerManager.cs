using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> spawners;

    private float spawnDelay;

    public void SetRoundData(GameRoundData roundData)
    {
        for (int i = 0; i < roundData.ActiveSpawnersCount; i++)
        {
            if (i == 0) { spawnDelay = 0f; }
            else { spawnDelay += roundData.SpawnDelay; }

            spawners[i].gameObject.SetActive(true);
            spawners[i].SetRoundDataSettings(roundData.TimeToFirstSpawn + spawnDelay, roundData.SpawnRate + spawnDelay);
        }
    }
}
