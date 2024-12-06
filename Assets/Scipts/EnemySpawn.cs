
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 10;
    public float spawnInterval = 1f;

    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemies.Count < maxEnemies)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemies.Add(newEnemy);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
