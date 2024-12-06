using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Slider player1HpBar;
    public Slider player2HpBar;

    public GameObject player1; // Drag and drop the Player 1 GameObject in the Inspector
    public GameObject player2; // Drag and drop the Player 2 GameObject in the Inspector

    private PlayerHealth player1Health;
    private PlayerHealth player2Health;

    public GameObject pauseMenuUI; // Assign the Pause Menu UI Canvas or Panel in the Inspector

    private bool isPaused = false;

    public Text exptext;
    
    public Expupdate expupdate;

    public GameObject gm;

    void Start()
    {
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

        expupdate = gm.GetComponent<Expupdate>();
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

        exptext.text = "Coins: " + expupdate.currentExp.ToString();
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