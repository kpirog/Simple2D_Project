using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private List<ItemBase> items;

    [Header("Drop settings")]
    [SerializeField] [Range(0f, 100f)] private float dropProbability = 50f;

    public void TryDropItem()
    {
        bool canDrop = Random.Range(0f, 100f) <= dropProbability;
        
        if (canDrop)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.z = 0f;

            Instantiate(items[Random.Range(0, items.Count)], spawnPos, Quaternion.identity);
        }
    }
}
