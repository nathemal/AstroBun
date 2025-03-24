using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialSpeechBubble : MonoBehaviour
{
    public GameObject speechBubblePanel;
    public TMP_Text speechBubbleText;
    public GameObject fuelMeter;
    public GameObject healthMeter; 
    public GameObject enemyPrefab;
    public Transform player;
    public GameObject planetPrefab; 
    public GameObject shopPrefab;

    private bool tutorialActive = true;
    private bool hasMoved = false;
    private bool dashPromptActive = false;
    private bool enemyPromptActive = false;
    private bool shootPromptActive = false;
    private bool lootTutorialTriggered = false;
    private bool shieldPromptActive = false; 
    private bool worldTutorialTriggered = false; 

    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>(); 
        ShowSpeechBubble("Welcome to the Tutorial. Use WASD or Arrow Keys to Move!");

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

        Time.timeScale = 0f; 

        HighlightFuelMeter(true);
        ShowSpeechBubble("This is your fuel meter. Keep an eye on it, movement costs Fuel!");
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;

        HighlightFuelMeter(false);
        HideSpeechBubble();

        StartCoroutine(WaitBeforeDashTutorial()); 
    }

    IEnumerator WaitBeforeDashTutorial()
    {
        yield return new WaitForSeconds(2f);

        ShowSpeechBubble("Press Left Shift to give yoursef a short burst of power!");
        dashPromptActive = true;
    }

    IEnumerator SpawnEnemyAndExplain()
    {
        yield return new WaitForSeconds(2f);

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

        ShowSpeechBubble("Watch out! There is an enemy ahead!");
        enemyPromptActive = true;
        yield return new WaitForSeconds(2f);  


        StartCoroutine(WaitBeforeHealthTutorial()); 
    }

    IEnumerator WaitBeforeHealthTutorial()
    {
        yield return new WaitForSeconds(1f); 

        Time.timeScale = 0f;  
        HighlightHealthMeter(true);
        ShowSpeechBubble("This is your health meter. If it reaches 0, you die!");
        yield return new WaitForSecondsRealtime(3f);

        HighlightHealthMeter(false); 
        HideSpeechBubble();

        Time.timeScale = 1f;  

   
        StartCoroutine(ShowAimingAndShootingTutorial());
    }


    IEnumerator ShowAimingAndShootingTutorial()
    {
 
        yield return new WaitForSeconds(0f);
        ShowSpeechBubble("Your ship is equiped with a Shield, press Right-Click to activate it");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1)); 
        HideSpeechBubble();

   
        ShowSpeechBubble("Now, press Right Click again to switch to your cannon, Left Click to shoot!");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1));  
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));  
        HideSpeechBubble();

        yield return new WaitForSeconds(0f);
        StartCoroutine(ShowLootAndProgressTutorial());

    }

    IEnumerator ShowLootAndProgressTutorial()
    {

        ShowSpeechBubble("When you kill an enemy, you have a chance to loot Fuel and Health while always gaining Gems.");
 
        yield return new WaitForSeconds(5f);
        ShowSpeechBubble("You progress levels by clearing all enemies, here just press E once you are done.");

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E)); 
        StartCoroutine(SpawnPlanetAndExplain());

     
        HideSpeechBubble();
    }

    IEnumerator SpawnPlanetAndExplain()
    {
        yield return new WaitForSeconds(2f);

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero; 
            playerRb.angularVelocity = 0f;
        }

        if (planetPrefab != null && player != null)
        {
            Vector3 spawnPosition = player.position + new Vector3(40f, 0f, 0f);
            Instantiate(planetPrefab, spawnPosition, Quaternion.identity);

        }

        ShowSpeechBubble("That is a planet! The ring around it represents it's gravitational pull.");
        yield return new WaitForSeconds(4f);

        HideSpeechBubble();
        ShowSpeechBubble("Once in orbit, hold Space to accelerate. Release to shoot out in the direction you're facing.");
       

        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        HideSpeechBubble();


        yield return new WaitForSeconds(1f);
        StartCoroutine(MovePlayerToSpecificPosition());
    }

    IEnumerator MovePlayerToSpecificPosition()
    {

        Vector3 newPosition = new Vector3(377f, 150f, 0f); 

        player.transform.position = newPosition;

        ShowSpeechBubble("This is a shop! Stand close and left-click to interact.");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        HideSpeechBubble();


        yield return new WaitForSeconds(0.3f);
        ShowSpeechBubble("These are powerups you can purchase with currency!");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        HideSpeechBubble();

        yield return new WaitForSeconds(2f);
        ShowSpeechBubble("Make sure you spend your money, as you lose it if you die!");
        HideSpeechBubble();

        yield return new WaitUntil(() =>
            Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D));

        HideSpeechBubble();


        ShowSpeechBubble("You are now ready to play the game.. Click anywhere to go to the level select screen.");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        HideSpeechBubble();



        SceneManager.LoadScene("LevelSelect");

    }



    public void ShowSpeechBubble(string message)
    {
        speechBubblePanel.SetActive(true);
        speechBubbleText.text = message;    }

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

    void HighlightHealthMeter(bool highlight)
    {
        if (healthMeter != null)
        {
            var healthImage = healthMeter.GetComponentInChildren<UnityEngine.UI.Image>();
            if (healthImage != null)
            {
                healthImage.color = highlight ? new Color(1f, 0.6f, 0.6f) : Color.white; 
            }
        }
    }
}
