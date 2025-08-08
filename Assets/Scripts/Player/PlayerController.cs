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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            ChangePauseState();

        if (gamePaused)
            return;

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !orbitController.isOrbiting && fuel > 0)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetMouseButtonDown(0) && canPlayerShoot) // left click
        {
            weapon.Fire();
            
            sound.playerShooting.Play();
        }

        if (Input.GetMouseButtonDown(1))
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

        if (!orbitController.isOrbiting)
        {
            if (useFuel && movementInput.sqrMagnitude > 0.1f)
            {
                rb.AddForce(movementInput.normalized * thrustForceWithFuel);

                fuel -= fuelConsumptionRate * Time.deltaTime;
                fuelTank.UpdateFuelTank(fuelTank.fuelBar.maxValue, fuel);
                data.FuelAmountValue = fuel;

                fuel = Mathf.Max(fuel, 0);
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
        fuel += amount;
        
        fuel = Mathf.Min(fuel, fuelTank.fuelBar.maxValue); //cap fuel according the fuel tank capacity

        fuelTank.UpdateFuelTank(fuelTank.fuelBar.maxValue, fuel);
    }

    private void AimDirectionRotation()
    {
        //rotate the player with the mouse
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