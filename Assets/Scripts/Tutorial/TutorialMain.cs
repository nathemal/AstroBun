using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialSpeechBubble : MonoBehaviour
{
    public GameObject speechBubblePanel;
    public TMP_Text speechBubbleText;
    public GameObject fuelMeter;
    public GameObject healthMeter;  // Reference for health meter
    public GameObject enemyPrefab;
    public Transform player;
    public GameObject planetPrefab; // Reference for planet prefab

    private bool tutorialActive = true;
    private bool hasMoved = false;
    private bool dashPromptActive = false;
    private bool enemyPromptActive = false;
    private bool shootPromptActive = false;
    private bool lootTutorialTriggered = false;
    private bool shieldPromptActive = false; // Flag for shield tutorial
    private bool worldTutorialTriggered = false; // Flag to trigger world tutorial

    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>(); // Get player's Rigidbody
        ShowSpeechBubble("Use WASD or Arrow Keys to Move!");
    }

    void Update()
    {
        if (tutorialActive && !hasMoved)
        {
            HandleMovementTutorial();
        }

        if (dashPromptActive && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashPromptActive = false;
            HideSpeechBubble();
            StartCoroutine(SpawnEnemyAndExplain());
        }

        if (shootPromptActive && Input.GetMouseButtonDown(0)) // Left mouse click
        {
            shootPromptActive = false;
            HideSpeechBubble();
        }

        if (shieldPromptActive && Input.GetMouseButtonDown(1)) // Right-click for shield
        {
            shieldPromptActive = false;
            HideSpeechBubble();
            Time.timeScale = 1f; // Unfreeze the game
            Debug.Log("Shield activated, game resumed!");

            // Trigger the world tutorial after shield activation
            StartCoroutine(SpawnPlanetAndExplain());
        }
    }

    private void HandleMovementTutorial()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            hasMoved = true;
            HideSpeechBubble();
            StartCoroutine(WaitBeforeFuelHighlight());
        }
    }

    IEnumerator WaitBeforeFuelHighlight()
    {
        yield return new WaitForSeconds(3f);

        Time.timeScale = 0f; // Pause the game
        Debug.Log("Game is frozen!");

        HighlightFuelMeter(true);
        ShowSpeechBubble("This is your fuel meter. Dashing and moving consume fuel!");
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f; // Unpause the game
        Debug.Log("Game unpaused!");

        HighlightFuelMeter(false);
        HideSpeechBubble();

        StartCoroutine(WaitBeforeDashTutorial());  // Move to dash tutorial after fuel
    }

    IEnumerator WaitBeforeDashTutorial()
    {
        yield return new WaitForSeconds(2f);

        ShowSpeechBubble("Hold Shift to Dash!");
        dashPromptActive = true;
    }

    IEnumerator SpawnEnemyAndExplain()
    {
        yield return new WaitForSeconds(2f);

        // Stop player momentum
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero; // Ensure player stops moving
            playerRb.angularVelocity = 0f;
        }

        // Spawn enemy
        if (enemyPrefab != null && player != null)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0) * 20f;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        // Show enemy tutorial
        ShowSpeechBubble("This is an enemy! Avoid collisions and destroy them.");
        enemyPromptActive = true;
        yield return new WaitForSeconds(3f);  // Wait before moving to health tutorial

        // Trigger health tutorial
        StartCoroutine(WaitBeforeHealthTutorial());  // Start health tutorial after the enemy tutorial
    }

    IEnumerator WaitBeforeHealthTutorial()
    {
        yield return new WaitForSeconds(2f);  // Wait for a moment before showing health tutorial

        Time.timeScale = 0f;  // Freeze the game for the health tutorial
        HighlightHealthMeter(true);  // Highlight health meter
        ShowSpeechBubble("This is your health meter. If it reaches 0, you die!");
        yield return new WaitForSecondsRealtime(3f);

        HighlightHealthMeter(false);  // Unhighlight health meter
        HideSpeechBubble();

        Time.timeScale = 1f;  // Unfreeze the game

        // Start shield tutorial
        StartCoroutine(ShieldTutorial());
    }

    // Shield Tutorial - Teach player about shield
    IEnumerator ShieldTutorial()
    {
        yield return new WaitForSeconds(2f);

        Time.timeScale = 0f; // Freeze the game for the shield tutorial

        ShowSpeechBubble("Right-click to activate your shield. It will block damage!");
        shieldPromptActive = true;
        yield return new WaitForSecondsRealtime(3f);

        // Wait until the player interacts with the shield (right-click)
        while (!shieldPromptActive)
        {
            yield return null;
        }

        // Once the player activates the shield
        Time.timeScale = 1f; // Unfreeze the game
        Debug.Log("Shield tutorial completed, game resumed!");
    }

    // Trigger Loot Tutorial
    public void TriggerLootTutorial()
    {
        if (!lootTutorialTriggered)
        {
            lootTutorialTriggered = true;
            StartCoroutine(LootTutorial());
        }
    }

    private IEnumerator LootTutorial()
    {
        Time.timeScale = 0f; // Freeze the game
        Debug.Log("Loot tutorial started and game is frozen!");

        ShowSpeechBubble("Enemies drop loot! Some restore health & fuel.");
        yield return new WaitForSecondsRealtime(3f);

        ShowSpeechBubble("This is your currency! Spend it in the shop.");
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f; // Unfreeze the game
        Debug.Log("Loot tutorial completed, game unpaused!");

        HideSpeechBubble();
    }

    // New Planet Tutorial
    IEnumerator SpawnPlanetAndExplain()
    {
        yield return new WaitForSeconds(2f);

        // Stop player momentum
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero; // Ensure player stops moving
            playerRb.angularVelocity = 0f;
        }

        // Spawn the planet 10 units from the player
        if (planetPrefab != null && player != null)
        {
            Vector3 spawnPosition = player.position + new Vector3(40f, 0f, 0f); 
            Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
        }

        // Show planet tutorial message
        ShowSpeechBubble("This is a planet! The ring around it represents its gravitational pull.");
        worldTutorialTriggered = true; // Mark world tutorial as triggered
        yield return new WaitForSeconds(4f);

        HideSpeechBubble();
        Time.timeScale = 1f; // Unfreeze the game
    }

    public void ShowSpeechBubble(string message)
    {
        speechBubblePanel.SetActive(true);
        speechBubbleText.text = message;
        Debug.Log("Showing speech bubble: " + message); // Debug log to check if it's being shown
    }

    public void HideSpeechBubble()
    {
        speechBubblePanel.SetActive(false);
        Debug.Log("Hiding speech bubble"); // Debug log to check if it's being hidden
    }

    void HighlightFuelMeter(bool highlight)
    {
        if (fuelMeter != null)
        {
            var fuelImage = fuelMeter.GetComponentInChildren<UnityEngine.UI.Image>();
            if (fuelImage != null)
            {
                fuelImage.color = highlight ? new Color(0.6f, 0.8f, 1f) : Color.white;
            }
        }
    }

    // New function to highlight health meter
    void HighlightHealthMeter(bool highlight)
    {
        if (healthMeter != null)
        {
            var healthImage = healthMeter.GetComponentInChildren<UnityEngine.UI.Image>();
            if (healthImage != null)
            {
                healthImage.color = highlight ? new Color(1f, 0.6f, 0.6f) : Color.white;  // Pale red color
            }
        }
    }
}
