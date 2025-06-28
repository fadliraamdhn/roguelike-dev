using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject enemyPrefab;
        public float spawnWeight = 1f; // for randomness
    }

    public List<SpawnEntry> enemies;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Clean up any destroyed enemies
            activeEnemies.RemoveAll(e => e == null);

            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = GetRandomEnemy();
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(newEnemy);
    }

    private GameObject GetRandomEnemy()
    {
        float totalWeight = 0f;
        foreach (var entry in enemies)
            totalWeight += entry.spawnWeight;

        float roll = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var entry in enemies)
        {
            cumulative += entry.spawnWeight;
            if (roll <= cumulative)
                return entry.enemyPrefab;
        }

        return enemies[0].enemyPrefab; // fallback
    }
}
