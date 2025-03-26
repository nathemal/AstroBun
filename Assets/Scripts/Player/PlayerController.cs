using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float thrustForceWithFuel = 5f;
    public float thrustForceWithoutFuel = 2f;
    public float maxSpeed = 15f;
    public float rotationSpeed = 80f;
    public bool shieldActive = false;

    //fuel
    public bool useFuel = false;
    public float fuel = 100f;
    public float fuelConsumptionRate = 10f;

    //dash
    public float dashForce = 15f;  
    public float dashDuration = 0.3f; 
    private bool isDashing = false;

    private bool canPlayerShoot = true;

    public PlayerAttack weapon;
    public GameObject gun;
    public GameObject shield;
    public Fuelbar fuelTank;

    OrbitController orbitController;
    Rigidbody2D rb;
    Vector2 movementInput;
    Vector2 mousePosition;

    private GameObject audioManager;
    private SoundManager sound;

    public GameObject pauseMenu;
    public bool gamePaused = false;

    [Header("Saving Player Data")]
    public PlayerData data;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager");

        sound = audioManager.GetComponent<SoundManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orbitController = GetComponent<OrbitController>();

        /* string currentSceneName = SceneManager.GetActiveScene().name;


         if (data.isNewGame)//if (data.lastSceneName == currentSceneName || (data.lastSceneName == ""))
         {
             UpdateFuelDataInNextScene();
         }
         else if(data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == ""))
         {
             UpdateFuelDataInFirstScene();

             //Debug.Log("inside in the if statement");
             //Debug.Log("current fuel amount " + fuel + " enemyData: " + data.FuelAmountValue);
             //Debug.Log("consumption rate " + fuelConsumptionRate + " enemyData: " + data.FueConsumptionValue);
             //Debug.Log("max fuel amount " + fuelTank.fuelBar.maxValue + " enemyData: " + data.FuelTankCapValue);
             //Debug.Log("max fuel amount " + fuelTank.fuelBar.maxValue + " current fuel amount: " + fuel);
         }*/

    }

    private void UpdateFuelDataInFirstScene()
    {
        fuel = data.FuelAmountValue;
        fuelConsumptionRate = data.FueConsumptionValue;

        fuelTank.UpdateFuelTank(data.FuelTankCapValue, fuel);
    }

    private void UpdateFuelDataInNextScene()
    {
        fuelTank.UpdateFuelTank(fuel, fuel);
        data.FuelTankCapValue = fuelTank.fuelBar.maxValue;
        data.FueConsumptionValue = fuelConsumptionRate;
        data.FuelAmountValue = fuel;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        AimDirectionRotation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePauseState();

        if (gamePaused)
            return;

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !orbitController.isOrbiting)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetMouseButtonDown(0) && canPlayerShoot) // left click
        {
            weapon.Fire();
            
            sound.playerShooting.Play();
        }

        if (Input.GetMouseButtonDown(1)) // right click
        {
            shield.SetActive(!shield.activeInHierarchy);
            
            shieldActive = !shieldActive;
            canPlayerShoot = !shieldActive;
            }

        if (Input.GetKeyDown(KeyCode.F))
            useFuel = !useFuel;
    }

    void ApplyMovement()
    {

        if (fuel <= 0)
            useFuel = false;

        /* if (movementInput.sqrMagnitude > 0.1f) // Moved this function here from Update to avoid tying any of the physics to fps
        {
            REASON: I commented this out because the program is confused - doesnt understand in which way the rigid body needs to rotate,  because I use same rigid body for aiming.
            I tried to add additional rigid body to gun, but when I playtested, it detached from the main body. I don't know if you want to rotate the main body differenty from the gun 'body' 

            Tried to work on different values for the player movemnt but it is really unresponsive, feels like you skate on ice, it is really annoying.

            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
        } */

        //Debug.Log("In applying movement");

        if (!orbitController.isOrbiting)
        {
            if (useFuel && movementInput.sqrMagnitude > 0.1f)
            {
                rb.AddForce(movementInput.normalized * thrustForceWithFuel);

                fuel -= fuelConsumptionRate * Time.deltaTime;
                fuelTank.UpdateFuelTank(fuelTank.fuelBar.maxValue, fuel);
                fuel = Mathf.Max(fuel, 0);
                
              // data.FuelAmountValue = fuel;

            }
            else if (!useFuel && movementInput.sqrMagnitude > 0.1f)
            {
                rb.AddForce(movementInput.normalized * thrustForceWithoutFuel);
            }
        }

        if (!isDashing)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    public void ChangePauseState()
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1;
        }

        else
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0;
        }
    }

    // Call this function to refuel the spaceship
    public void Refuel(float amount)
    {
        //Debug.Log("Current fuel amount before addition: " + fuel);
        //Debug.Log("Added amount is " + amount);

        fuel += amount;
        //Debug.Log("fuel amount AFTER without cap limit: " + fuel);

        //fuel = Mathf.Min(fuel, 100f); // Cap fuel at max
        fuel = Mathf.Min(fuel, fuelTank.fuelBar.maxValue); //cap fuel according the fuel tank capacity

        fuelTank.UpdateFuelTank(fuelTank.fuelBar.maxValue, fuel);
    }

    //Kamile added this
    private void AimDirectionRotation()
    {
        //rotate the player / gun with the mouse
        Vector3 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void DisableShooting()
    {
        canPlayerShoot = false;
    }

    public void EnableShooting()
    {
        canPlayerShoot = true;
    }

    IEnumerator Dash()
    {
        isDashing = true;

        // Capture movement direction
        Vector2 dashDirection = movementInput.normalized;

        // Apply impulse force only if the player is moving
        if (dashDirection != Vector2.zero)
        {
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}