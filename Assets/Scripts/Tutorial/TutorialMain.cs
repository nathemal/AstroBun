using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialSpeechBubble : MonoBehaviour
{
    public GameObject speechBubblePanel; // UI panel for speech bubble
    public TMP_Text speechBubbleText;    // Speech bubble text
    public GameObject fuelMeter;         // Fuel meter to highlight

    private bool tutorialActive = true;  // Track if tutorial is running
    private bool hasMoved = false;       // Track if the player has moved

    void Start()
    {
        ShowSpeechBubble("Use WASD or Arrow Keys to Move!");
    }

    void Update()
    {
        if (tutorialActive && !hasMoved)
        {
            // Check if the player moves
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                hasMoved = true;
                HideSpeechBubble();  // Hide the initial speech bubble

                // Start the tutorial sequence for fuel meter
                StartCoroutine(WaitBeforeFuelHighlight());
            }
        }
    }

    IEnumerator WaitBeforeFuelHighlight()
    {
        // Let the player move for 3 seconds before highlighting the fuel meter
        yield return new WaitForSeconds(3f);

        // Freeze the game time so the player can't move
        Time.timeScale = 0f;

        // Highlight the fuel meter
        HighlightFuelMeter(true);

        // Show a speech bubble explaining the fuel meter
        ShowSpeechBubble("This is your fuel meter. Dashing and moving consume fuel!");

        // Wait for 3 seconds while game is frozen
        yield return new WaitForSecondsRealtime(3f);

        // Unfreeze the game so the player can continue
        Time.timeScale = 1f;

        // Remove fuel highlight
        HighlightFuelMeter(false);

        // Hide the speech bubble
        HideSpeechBubble();
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
            // Get the Image component of the Slider's fill area (if needed)
            var fuelImage = fuelMeter.GetComponentInChildren<UnityEngine.UI.Image>();
            if (fuelImage != null)
            {
                fuelImage.color = highlight ? new Color(0.6f, 0.8f, 1f) : Color.white;
            }
        }
    }
}
