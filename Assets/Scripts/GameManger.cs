using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;  // For UI elements
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public CanvasController canvasController; // Reference to the CanvasController script
    public GameObject player;

    public  GameObject checkplayer1,checkplayer2;
    public bool iswingame = false;

    public bool isgo = true;

     public int total_i=0;

    public int totalEnemyCount = -1;
    
      public bool isreturnMain =false;


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
        if(checkplayer1!=null){
        checkplayer1=GameObject.Find("1PlayerArmature");}

        if(checkplayer2!=null){
        checkplayer2=GameObject.Find("2PlayerArmature");}
        waveTimer = startWaveTime; // Initialize the wave timer
        // Start the initial wave countdown in the CanvasController
        if (canvasController != null)
        {
            canvasController.StartNewWaveCountdown(waveTimer);
        }
        new WaitForSeconds(timeBetweenWaves);


        //if (!isstop){
        StartCoroutine(WaveManager());
       // }
    }

    void Update()

    {

      
         if (checkplayer1==null&&checkplayer2==null)
        {
            iswingame=false;
            if(!isreturnMain){
            canvasController.WinGame();}
         
        }
        if (total_i == totalEnemyCount && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    isgo = true;
                    total_i = 0;
                    totalEnemyCount = 0;
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
                // Notify the CanvasController to restart the wave countdown
                if(isgo){
                yield return StartCoroutine(StartWave(waves[currentWaveIndex]));}
            }
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
            totalEnemyCount += enemySettings.enemyCount;
        }

        // Wait until all enemies are defeated
        while (waveInProgress)
        {
            yield return new WaitForSeconds(1f);
            if (isgo&&GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {

                
                waveInProgress = false;
                Debug.Log($"Wave {wave.waveName} Completed! Transitioning to the shop menu.");
                
                CallWaveUI();

                if (!waveInProgress && currentWaveIndex == waves.Count - 1)
                {
                    Debug.Log("All waves completed and all enemies destroyed. You win!");
                    iswingame=true;
                     if(!isreturnMain){
                    canvasController.WinGame();}
                    break;
                }
                else 
                {
                canvasController.CallShopMenu();
                yield return new WaitForSeconds(timeBetweenWaves);
                WaveManager();
                break;
                }
                
            }
            yield return null;
        }
    }
        private IEnumerator SpawnEnemies(EnemySettings settings)
        {
            isgo=false;
           
            
           

            for ( int i= 0; i < settings.enemyCount; i++)
            {
                


                Vector3 spawnPoint = RandomNavMeshLocation(radius, "AvoidTagHere");

                if (spawnPoint != Vector3.zero && Vector3.Distance(spawnPoint, player.transform.position) > minEnemyDistance)
                {
                   
                   // yield return new WaitForSeconds(1f);
                    Instantiate(settings.enemyPrefab, spawnPoint, Quaternion.identity);
                   
                }
                 total_i++;
               

                yield return new WaitForSeconds(settings.spawnInterval);
               
                Debug.Log("total_i:"+total_i+"totalEnemyCount"+totalEnemyCount);
               
            }
            
          
        }

        private Vector3 RandomNavMeshLocation(float radius, string avoidTag)
        {     
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            Vector3 randomPoint = Vector3.zero;
            NavMeshHit hit;

            do
            {
                randomPoint = Random.insideUnitSphere * radius + player.transform.position;

                if (NavMesh.SamplePosition(randomPoint, out hit, radius, 1))
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.position, 1f); // 檢查生成位置周圍1個單位內的碰撞體

                    bool avoidPosition = false;
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag(avoidTag))
                        {
                            avoidPosition = true;
                            break;
                        }
                    }

                    if (!avoidPosition)
                    {
                        return hit.position; // 如果生成位置不在特定tag的位置，返回該位置
                    }
                }
            } while (true);
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
