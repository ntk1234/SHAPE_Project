using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CanvasController : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager script

    public float turntime ;

    [Header("Set Player")]
    public GameObject player1; // Drag and drop the Player 1 GameObject in the Inspector
    public GameObject player2; // Drag and drop the Player 2 GameObject in the Inspector

    public PlayerHealth player1Health;
    public PlayerHealth player2Health;

    public CharController cc;
    public CharController1 cc1;

    [Header("Set HP Bar")]
    public Slider player1HpBar;
    public Slider player2HpBar;
    public float hpBarLerpSpeed = 5f; // Speed of the HP bar interpolation

     [Header("Set Mp Bar")]
    public Slider player1MpBar;
    public Slider player2MpBar;
    //public Slider player2MpBar;
    public float mpBarLerpSpeed = 5f; // Speed of the HP bar interpolation

    [Header("Set Score Text")]
    public Text scoreText; // Reference to the Text component to display the score

    [Header("PauseMenu")]
    public GameObject pauseMenuUI; // Assign the Pause Menu UI Canvas or Panel in the Inspector
    private bool isPaused = false;

    [Header("Experience Display")]
    public Text exptext; // Text element to display experience or coins

    public Expupdate expupdate; // Reference to the Expupdate script
    public GameObject gm; // GameManager or related object holding Expupdate

    [Header("Wave Timer")]
    public Text waveTimerText; // UI Text element to display the wave timer

    public float countdownTimer; // Timer for countdown
    private bool isCountingDown; // Flag to track if countdown is active

    [Header("Win Game")]
    public GameObject winGameUI;

    public Text _1pAllScore;
    public Text _2pAllScore;
    
    public Text grade;
    public Text _1pTitle;
    public Text _2pTitle;

    public string _1pstr=" ";
     public string _2pstr=" ";

     public string _1pIslive_str=" ";
     public string _2pIslive_str=" ";

    [Header("Shop Menu")]
    public GameObject shopMenuUI;



   

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Assign the GameManager component

        cc = player1.GetComponent<CharController>();
        cc1 = player2.GetComponent<CharController1>();

        if (gameManager != null)
        {
            countdownTimer = gameManager.startWaveTime; // Initialize with the start wave time
            isCountingDown = true; // Begin the countdown
        }

        // Get PlayerHealth components from the assigned GameObjects
        if (player1 != null)
        {
            player1Health = player1.GetComponent<PlayerHealth>();

          
            if (player1Health != null)
            {
                player1HpBar.maxValue = player1Health.maxHealth; // Set slider max value
                player1HpBar.value = player1Health.currentHealth; // Initialize with current health
            }

            
            if (cc != null)
            {
                player1MpBar.maxValue = cc.maxMp; // Set slider max value
                player1MpBar.value = cc.currMp; // Initialize with current health
            }
        }

        if (player2 != null)
        {
            player2Health = player2.GetComponent<PlayerHealth>();
            if (player2Health != null)
            {
                player2HpBar.maxValue = player2Health.maxHealth; // Set slider max value
                player2HpBar.value = player2Health.currentHealth; // Initialize with current health
            }

             if (cc != null)
            {
                player2MpBar.maxValue = cc1.maxMp; // Set slider max value
                player2MpBar.value = cc1.currMp; // Initialize with current health
            }
        }

        if (gm != null)
        {
            expupdate = gm.GetComponent<Expupdate>();
        }

        if (gameManager != null)
        {
            countdownTimer = gameManager.startWaveTime; // Initialize with the start wave time
            isCountingDown = true; // Begin the countdown
        }
        // if (player1 != null)
       // {
           
       // }


        // Initialize score display
        UpdateScoreDisplay(0); // Assuming the initial score is 0
    }

    void Update()
    {

        // Smoothly update the health bars based on the players' current health
        if (player1Health != null)
        {
            player1HpBar.value = Mathf.Lerp(player1HpBar.value, player1Health.currentHealth, Time.deltaTime * hpBarLerpSpeed);
        }

        if (player2Health != null)
        {
            player2HpBar.value = Mathf.Lerp(player2HpBar.value, player2Health.currentHealth, Time.deltaTime * hpBarLerpSpeed);
        }

         if (cc!= null)
        {
            player1MpBar.value = Mathf.Lerp(player1MpBar.value, cc.currMp,Time.deltaTime * mpBarLerpSpeed);
        }

         if (cc1!= null)
        {
            player2MpBar.value = Mathf.Lerp(player2MpBar.value, cc1.currMp,Time.deltaTime * mpBarLerpSpeed);
        }
        // Update the experience text
        if (exptext != null && expupdate != null)
        {
            exptext.text = "Coins: " + expupdate.currentExp.ToString();
        }
       
        // Countdown logic for wave timer
        if (isCountingDown)
        {
            countdownTimer -= Time.deltaTime;

            if (waveTimerText != null)
            {
                SendWaveText();
            }

            if (countdownTimer <= 0)
            {
                countdownTimer = 0; // Ensure it doesn't go below 0
                isCountingDown = false; // Stop the countdown
            }
        }

        // Check for pause toggle (default key: Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        turntime+=Time.deltaTime;
       
    }

    public void UpdateScoreDisplay(int _score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + _score;
        }
    }

    public void SendWaveText()

    {

        waveTimerText.text = countdownTimer > 0
        ? $"Next Wave In: {Mathf.CeilToInt(countdownTimer)}s"
        : "Wave in Progress!";

    }

    // Method to start the countdown for the next wave
    public void StartNewWaveCountdown(float waveStartTime)
    {
        countdownTimer = waveStartTime; // Reset timer for the next wave
        isCountingDown = true; // Restart the countdown
    }

    public void OnWaveCompleted()
    {
        // Reset timer and start the countdown for the next wave
        StartNewWaveCountdown(gameManager.timeBetweenWaves);
    }

    public void CallShopMenu()
    {
        Time.timeScale = 0f; // Freeze the game
        shopMenuUI.SetActive(true); // Show the pause menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void ResumeShopMenu()
    {

        Time.timeScale = 1f; // Resume the game
        shopMenuUI.SetActive(false); // Hide the pause menu
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    public void WinGame()
    {
        Time.timeScale = 0f; // Freeze the game
        winGameUI.SetActive(true); // Show the pause menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible


        if(turntime<=120f)
        {
            grade.text="S";
        }
        else if(turntime<=180f)
        {
            grade.text="A";
        }
        else if(turntime<=200f)
        {
            grade.text="B";
        }
        else
        {
           grade.text="C"; 
        }


        _1pAllScore.text = "Score: " + gameManager._1pscore.ToString();
        _2pAllScore.text = "Score: " + gameManager._2pscore.ToString();
        
        
         if (gameManager._1pscore > gameManager._2pscore)
        {
                Debug.Log("Player 1's score is higher. AAA");
                _1pstr="top scorer";
        }
        else if (gameManager._2pscore > gameManager._1pscore)
        {
                Debug.Log("Player 2's score is higher. AAA");
                _2pstr="top scorer";
        }
        else
        {
                Debug.Log("Both players have the same score. AAA");
                _1pstr="";
                _2pstr="";
        }

        if(player1==null)
        {
            _1pIslive_str = "Death!!!";
        }
        else{
            _1pIslive_str ="Survived!!";
        }

        if(player2==null)
        {
            _2pIslive_str = "Death!!!";
        }
        else{
            _2pIslive_str ="Survived!!";
        }



        _1pTitle.text = _1pIslive_str +"\n"+ _1pstr;
        _2pTitle.text = _2pIslive_str +"\n"+ _2pstr;
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freeze the game
        pauseMenuUI.SetActive(true); // Show the pause menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Quit the application (won't work in the Unity editor)
    }
}
