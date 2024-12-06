using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    [Header("Spawn Settings")]
    public float radius = 30f;
    public float minEnemyDistance = 5f;

    [System.Serializable]
    public class EnemySettings
    {
        public GameObject enemyPrefab;
        public float enemyStartTime = 1f;
        public float enemyRepeatTime = 2f;
        public int maxEnemyNo = 10;
    }

    public List<EnemySettings> enemyList; // List of enemy settings

    private Dictionary<GameObject, int> enemySpawnCounts; // Tracks current spawn counts for each enemy

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySpawnCounts = new Dictionary<GameObject, int>();

        foreach (var enemySetting in enemyList)
        {
            enemySpawnCounts[enemySetting.enemyPrefab] = 0;
            StartCoroutine(SpawnEnemyRoutine(enemySetting));
        }
    }

    private IEnumerator SpawnEnemyRoutine(EnemySettings settings)
    {
        yield return new WaitForSeconds(settings.enemyStartTime);

        while (enemySpawnCounts[settings.enemyPrefab] < settings.maxEnemyNo)
        {
            SpawnEnemy(settings);
            yield return new WaitForSeconds(settings.enemyRepeatTime);
        }
    }

    private void SpawnEnemy(EnemySettings settings)
    {
        Vector3 spawnPoint = RandomNavMeshLocation(radius);

        if (spawnPoint != Vector3.zero
            && Vector3.Distance(spawnPoint, player.transform.position) > minEnemyDistance)
        {
            Instantiate(settings.enemyPrefab, spawnPoint, Quaternion.identity);
            enemySpawnCounts[settings.enemyPrefab]++;
        }
    }

    private Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius + player.transform.position;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, radius, 1))
        {
            return hit.position;
        }

        return Vector3.zero;
    }
}
