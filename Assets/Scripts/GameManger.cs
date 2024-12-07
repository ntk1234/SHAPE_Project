using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private GameObject player;

    [Header("Wave Settings")]
    public float startWaveTime = 5f; // Delay before the first wave starts
    public float timeBetweenWaves = 10f; // Delay between waves
    private int currentWaveIndex = -1;
    private bool waveInProgress = false;
    public List<Wave> waves; // List of waves

    private float waveTimer; // Timer for the wave countdown
    private bool isWaveStartCountdown = true; // Flag to track countdown state

    [Header("Spawn Settings")]
    public float radius = 30f;
    public float minEnemyDistance = 5f;

    public CanvasController canvasController; // Reference to the CanvasController script

    [System.Serializable]
    public class EnemySettings
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnInterval;
    }

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemySettings> enemies; // Enemies to spawn in this wave
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waveTimer = startWaveTime; // Initialize the wave timer

        // Start the initial wave countdown in the CanvasController
        if (canvasController != null)
        {
            canvasController.StartNewWaveCountdown(waveTimer);
        }
    }

    void Update()
    {
        // Manage the wave timer countdown
        if (isWaveStartCountdown)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                waveTimer = 0; // Clamp the timer to 0
                isWaveStartCountdown = false; // End the countdown
                StartCoroutine(WaveManager()); // Start wave management
            }
        }
    }

    public float GetWaveTimer()
    {
        return isWaveStartCountdown ? Mathf.Max(0, waveTimer) : 0f;
    }

    private IEnumerator WaveManager()
    {
        while (true)
        {
            if (!waveInProgress && currentWaveIndex < waves.Count - 1)
            {
                currentWaveIndex++;
                waveInProgress = true;
                canvasController.SendWaveText();
                 while (canvasController.countdownTimer > 0)
            {
                yield return null;
            }

                yield return StartCoroutine(StartWave(waves[currentWaveIndex]));

                // Wait for timeBetweenWaves after the wave ends
                waveTimer = timeBetweenWaves;
                isWaveStartCountdown = true;

                // Notify the CanvasController to restart the wave countdown
                if (canvasController != null)
                {
                    canvasController.StartNewWaveCountdown(waveTimer);
                }
            }

            yield return null;
        }
    }

    private IEnumerator StartWave(Wave wave)
    {
        Debug.Log($"Starting Wave: {wave.waveName}");

        foreach (var enemySettings in wave.enemies)
        {
            StartCoroutine(SpawnEnemies(enemySettings));
        }

        // Wait until all enemies are defeated
        while (waveInProgress)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                waveInProgress = false;
                Debug.Log($"Wave {wave.waveName} Completed!");
            }

            yield return null;
        }
    }

    private IEnumerator SpawnEnemies(EnemySettings settings)
    {
        for (int i = 0; i < settings.enemyCount; i++)
        {
            Vector3 spawnPoint = RandomNavMeshLocation(radius);

            if (spawnPoint != Vector3.zero
                && Vector3.Distance(spawnPoint, player.transform.position) > minEnemyDistance)
            {
                Instantiate(settings.enemyPrefab, spawnPoint, Quaternion.identity);
            }

            yield return new WaitForSeconds(settings.spawnInterval);
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
