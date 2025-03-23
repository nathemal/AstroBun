using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialSpeechBubble : MonoBehaviour
{
    public GameObject speechBubblePanel;
    public TMP_Text speechBubbleText;
    public GameObject fuelMeter;
    public GameObject enemyPrefab;
    public Transform player;

    private bool tutorialActive = true;
    private bool hasMoved = false;
    private bool dashPromptActive = false;
    private bool enemyPromptActive = false;
    private bool shootPromptActive = false;

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
    }

    IEnumerator WaitBeforeFuelHighlight()
    {
        yield return new WaitForSeconds(3f);

        Time.timeScale = 0f;
        HighlightFuelMeter(true);
        ShowSpeechBubble("This is your fuel meter. Dashing and moving consume fuel!");
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
        HighlightFuelMeter(false);
        HideSpeechBubble();

        StartCoroutine(WaitBeforeDashTutorial());
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
            playerRb.linearVelocity = Vector2.zero;
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
        yield return new WaitForSeconds(3f);

        // Show shooting tutorial
        ShowSpeechBubble("Aim with your mouse and click to shoot!");
        shootPromptActive = true;
    }

    public void ShowSpeechBubble(string message)
    {
        speechBubblePanel.SetActive(true);
        speechBubbleText.text = message;
    }

    public void HideSpeechBubble()
    {
        speechBubblePanel.SetActive(false);
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
}
