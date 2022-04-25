using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    private EnemyData enemyData;

    public void Init(List<GameObject> enemyPrefab, int minEnemyCount, int maxEnemyCount, int minTimeEnemySpawn, int maxTimeEnemySpawn)
    {
        enemyData = new EnemyData(enemyPrefab, minEnemyCount, maxEnemyCount, minTimeEnemySpawn, maxTimeEnemySpawn);
        StartCoroutine(TrySpawnEnemy(enemyData));
    }

    private void OnEnable()
    {
        EventBus.EnemyDestroyed += CheckDestroyedEnemy;
        EventBus.EndGame += DestroySpawner;
    }

    private void OnDisable()
    {
        EventBus.EnemyDestroyed -= CheckDestroyedEnemy;
        EventBus.EndGame -= DestroySpawner;
    }

    IEnumerator TrySpawnEnemy(EnemyData enemyData)
    {
        while (enemyData.SpawnedEnemy.Count < enemyData.MinEnemyCount)
        {
            SpawnEnemy(enemyData.EnemyPrefab);
        }

        float timeDelay = Random.Range(enemyData.MinTimeEnemySpawn, enemyData.MaxTimeEnemySpawn);
        yield return new WaitForSeconds(timeDelay);
        if (CanEnemySpawn(enemyData.SpawnedEnemy.Count, enemyData.MaxEnemyCount))
        {
            SpawnEnemy(enemyData.EnemyPrefab);
        }
        StartCoroutine(TrySpawnEnemy(enemyData));
    }

    private bool CanEnemySpawn(int curCount, float maxCount)
    {
        return curCount < maxCount;
    }

    private void SpawnEnemy(List<GameObject> enemyPrefab)
    {
        GameObject curEnemyPrefab = enemyPrefab[Random.Range(0, enemyPrefab.Count)];
        Vector3 spawnPosition = CalcSpawnPosition(MainCamera.CameraLeftBottomPoint, MainCamera.CameraRightTopPoint, curEnemyPrefab);
        GameObject curEnemySpawn = Instantiate(curEnemyPrefab, spawnPosition, Quaternion.identity);
        enemyData.AddSpawnedEnemy(curEnemySpawn);
    }

    private Vector3 CalcSpawnPosition(Vector3 CameraLeftBottomPoint, Vector3 cameraRightTopPoint, GameObject spawnObject)
    {
        Vector2 size = CommonFunctions.CalcSize(spawnObject);
        float borderSpace = size.x > size.y ? size.x / 2 : size.y / 2;

        int horizontalSidePosition = Random.Range(-1, 1);
        int verticalSidePosition;
        float spawnPositionX;
        float spawnPositionY;
        if ( horizontalSidePosition == 0)
        {
            verticalSidePosition = Random.Range(-1, 1) >= 0 ? 1 : -1;
        }
        else
        {
            verticalSidePosition = 0;
        }

        spawnPositionX = horizontalSidePosition != 0 ? ((horizontalSidePosition == -1 ? CameraLeftBottomPoint.x : cameraRightTopPoint.x) + horizontalSidePosition * borderSpace)
                                                     : (Random.Range(CameraLeftBottomPoint.x, cameraRightTopPoint.x));
        spawnPositionY = verticalSidePosition != 0 ? ((verticalSidePosition == -1 ? CameraLeftBottomPoint.y : cameraRightTopPoint.y) + verticalSidePosition * borderSpace)
                                                     : (Random.Range(CameraLeftBottomPoint.y, cameraRightTopPoint.y));

        return new Vector3(spawnPositionX, spawnPositionY, 0);
    }

    private void CheckDestroyedEnemy(GameObject enemy, int scoreForEnemy)
    {
        for (int i = 0; i < enemyData.SpawnedEnemy.Count; i++)
        {
            if (enemy.Equals(enemyData.SpawnedEnemy[i]))
            {
                enemyData.RemoveSpawnedEnemy(enemy);
                return;
            }
        }
    }

    private void DestroySpawner()
    {
        Destroy(this.gameObject);
    }
}

