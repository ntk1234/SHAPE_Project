using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager script

    [Header("Set Player")]
    public GameObject player1; // Drag and drop the Player 1 GameObject in the Inspector
    public GameObject player2; // Drag and drop the Player 2 GameObject in the Inspector

    private PlayerHealth player1Health;
    private PlayerHealth player2Health;

    [Header("Set HP Bar")]
    public Slider player1HpBar;
    public Slider player2HpBar;

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

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Assign the GameManager component
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
        }

        if (player2 != null)
        {
            player2Health = player2.GetComponent<PlayerHealth>();
            if (player2Health != null)
            {
                player2HpBar.maxValue = player2Health.maxHealth; // Set slider max value
                player2HpBar.value = player2Health.currentHealth; // Initialize with current health
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
    }

    void Update()
    {
        // Update the health bars based on the players' current health
        if (player1Health != null)
        {
            player1HpBar.value = player1Health.currentHealth;
        }

        if (player2Health != null)
        {
            player2HpBar.value = player2Health.currentHealth;
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
