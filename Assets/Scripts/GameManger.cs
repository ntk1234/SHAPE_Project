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

    public bool isStopSpwan = false;

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

    private Coroutine activeSpawnCoroutine; // Track the spawn coroutine

    [System.Serializable]
    public class EnemySettings
    {
        public GameObject enemyPrefab;
        public int maxEnemyCount; // Maximum number of this enemy type in the scene
        public float spawnInterval;
    }

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemySettings> enemies; // Enemies to spawn in this wave
        public int targetScore; // Score target to complete the wave
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

    
        if(!isStopSpwan){
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

         if (canvasController != null)
        {
            score = _1pscore+_2pscore;
            canvasController.UpdateScoreDisplay(score);
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
            
            if (!waveInProgress && currentWaveIndex < waves.Count-1 )
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

                if (!iswingame && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) // Check if it's the first wave
                {
                    Debug.Log("wave completed! Transitioning to the shop menu.");
                    canvasController.CallShopMenu();
                }
             
                // Notify the CanvasController to restart the wave countdown
                if (canvasController != null)
                {
                    canvasController.StartNewWaveCountdown(waveTimer);
                }
            }
           else if (!waveInProgress && currentWaveIndex ==  waves.Count-1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                Debug.Log("All waves completed and all enemies destroyed. You win!");
                iswingame = true;
                canvasController.WinGame();

                
            }

            yield return null;
        }
    }

    private IEnumerator StartWave(Wave wave)
    {
        Debug.Log($"Starting Wave: {wave.waveName}");

        // Start centralized spawn coroutine
        activeSpawnCoroutine = StartCoroutine(SpawnEnemies(wave));

        // Wait until the wave score target is met
        while (waveInProgress)
        {
            if (score >= wave.targetScore)
            {
                waveInProgress = false;
                Debug.Log($"Wave {wave.waveName} Completed!");
                canvasController.CleareEnemyText();
                // Stop the active spawn coroutine
                if (activeSpawnCoroutine != null)
                {
                    StopCoroutine(activeSpawnCoroutine);
                }
            }

            yield return null;
        }

        // Ensure all enemies are cleared before transitioning
         while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
         {
            yield return null;
        }

        Debug.Log($"Wave {wave.waveName} fully cleared!");
    }


    private IEnumerator SpawnEnemies(Wave wave)
{
    Dictionary<GameObject, int> activeEnemies = new Dictionary<GameObject, int>();
    foreach (var enemy in wave.enemies)
    {
        activeEnemies[enemy.enemyPrefab] = 0;
    }

    while (waveInProgress)
    {
        foreach (var enemySettings in wave.enemies)
        {
            int activeCount = CountActiveEnemies(enemySettings.enemyPrefab);
            activeEnemies[enemySettings.enemyPrefab] = activeCount;

            // Check if the number of active enemies is below the maximum allowed
            if (activeCount < enemySettings.maxEnemyCount)
            {
                Vector3 spawnPoint = RandomNavMeshLocation(radius);

                if (spawnPoint != Vector3.zero && Vector3.Distance(spawnPoint, player.transform.position) > minEnemyDistance)
                {
                    // Generate a random index to select a random enemy prefab
                    int randomIndex = Random.Range(0, wave.enemies.Count);
                    GameObject enemyPrefab = wave.enemies[randomIndex].enemyPrefab;

                    Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                    Debug.Log($"Spawned {enemyPrefab.name}. Active count: {activeCount + 1}/{enemySettings.maxEnemyCount}");
                }
            }
        }

        // Wait for a unified spawn interval
        yield return new WaitForSeconds(1f);
    }
    Debug.Log($"Wave {wave.waveName} enemies.");
    Debug.Log("Stopped spawning enemies.");
}

    private int CountActiveEnemies(GameObject enemyPrefab)
    {
        return GameObject.FindGameObjectsWithTag(enemyPrefab.tag).Length;
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

    
    }
}
