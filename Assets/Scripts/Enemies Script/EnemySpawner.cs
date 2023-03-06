using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject blueEnemyPrefab;
    [SerializeField] private GameObject redEnemyPrefab;
    
    public List<GameObject> enemies;

    private float spawnTimer = 7f;
    private int enemyCount = 0;
    private int blueEnemyCount = 0;
    private int redEnemyCount = 0;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnEnemies();
            spawnTimer = Mathf.Max(7f, spawnTimer - 0.5f);
        }
    }

    private void SpawnEnemies()
    {
        blueEnemyCount++;
        var blueEnemy = Instantiate(blueEnemyPrefab, GetSpawnPosition(), Quaternion.identity);
        enemies.Add(blueEnemy);

        for (int i = 0; i < 4; i++)
        {
            redEnemyCount++;
            var redEnemy = Instantiate(redEnemyPrefab, GetSpawnPosition(), Quaternion.identity);
            enemies.Add(redEnemy);
        }
        enemyCount = blueEnemyCount + redEnemyCount * 4;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1f;
        spawnPosition.x += Random.Range(-20.0f, 20.0f);
        spawnPosition.z += Random.Range(-20.0f, 20.0f);
        return spawnPosition;
    }
}
