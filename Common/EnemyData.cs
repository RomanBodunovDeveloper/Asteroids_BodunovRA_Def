using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public List<GameObject> EnemyPrefab { get; private set; }
    public List<GameObject> SpawnedEnemy { get; private set; }
    public int MinEnemyCount { get; private set; }
    public int MaxEnemyCount { get; private set; }
    public float MinTimeEnemySpawn { get; private set; }
    public float MaxTimeEnemySpawn { get; private set; }
    public EnemyData(List<GameObject> enemyPrefab, int minEnemyCount, int maxEnemyCount, float minTimeEnemySpawn, float maxTimeEnemySpawn)
    {
        EnemyPrefab = enemyPrefab;
        MinEnemyCount = minEnemyCount;
        MaxEnemyCount = maxEnemyCount;
        MinTimeEnemySpawn = minTimeEnemySpawn;
        MaxTimeEnemySpawn = maxTimeEnemySpawn;
        SpawnedEnemy = new List<GameObject>();
    }

    public void AddSpawnedEnemy(GameObject enemy)
    {
        SpawnedEnemy.Add(enemy);
    }

    public void RemoveSpawnedEnemy(GameObject enemy)
    {
        SpawnedEnemy.Remove(enemy);
    }
}
