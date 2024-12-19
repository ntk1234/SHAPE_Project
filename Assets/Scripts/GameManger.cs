using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;  // For UI elements

public class GameManager : MonoBehaviour
{
    public CanvasController canvasController; // Reference to the CanvasController script
    private GameObject player;
    private bool iswingame = false;

    

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

    [Header("Score Settings")]
    public int score = 0; // Score variable
    public int _1pscore = 0;
    public int _2pscore = 0;

    [System.Serializable]
    public class EnemySettings
    {
        public GameObject enemyPrefab;
        public int enemyCount; // Maximum number of this enemy type in the scene
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
                //waveTimer = 0; // Clamp the timer to 0
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
                while (canvasController.countdownTimer > 0)
                {
                    yield return null;
                }
                // Wait for timeBetweenWaves after the wave ends     
                           

                // Notify the CanvasController to restart the wave countdown
                yield return StartCoroutine(StartWave(waves[currentWaveIndex]));
            }
            else if (!waveInProgress && currentWaveIndex == waves.Count - 1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                Debug.Log("All waves completed and all enemies destroyed. You win!");
                iswingame = true;
                canvasController.WinGame();
            }
            //yield return null;
        }
    }

    void CallWaveUI()
    {
        canvasController.SendWaveText();
        canvasController.StartNewWaveCountdown(waveTimer);
    }

    private IEnumerator StartWave(Wave wave)
    {
        isWaveStartCountdown = false; // End the countdown
        Debug.Log($"Starting Wave: {wave.waveName}");
        waveTimer = timeBetweenWaves;
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
                Debug.Log($"Wave {wave.waveName} Completed! Transitioning to the shop menu.");
                CallWaveUI();
                canvasController.CallShopMenu();
                yield return new WaitForSeconds(timeBetweenWaves);
                isWaveStartCountdown = true;
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
    

    // Call this method to add points when an enemy is defeated
    public void AddScore(int points,int playerID)
    {
        

            if (playerID == 1)
        {
            _1pscore += points;
              Debug.Log($"1P SCORE {_1pscore}");
        
        }
        else if (playerID == 2)
        {
            _2pscore += points;
            // 如果需要，更新2P玩家的UI
              Debug.Log($"2P SCORE {_2pscore}");
        }

        if (canvasController != null)
        {
            score = _1pscore+_2pscore;
            canvasController.UpdateScoreDisplay(score);
        }
    }
}
