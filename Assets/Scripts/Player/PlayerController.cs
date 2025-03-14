using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float thrustForceWithFuel = 5f;
    public float thrustForceWithoutFuel = 2f;
    public float maxSpeed = 15f;
    public float rotationSpeed = 80f;
    public bool shieldActive = false;
    public bool useFuel = true;
    public float fuel = 100f;
    public float fuelConsumptionRate = 10f;

    private bool canPlayerShoot = true;

    public PlayerAttack weapon;
    public GameObject gun;
    public GameObject shield;
    public Fuelbar fuelTank;

    OrbitController orbitController;
    Rigidbody2D rb;
    Vector2 movementInput;
    Vector2 mousePosition;

    [Header("Saving Player Data")]
    public PlayerData data;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orbitController = GetComponent<OrbitController>();

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == ""))
        {
            fuel = data.FuelAmountValue;
            fuelConsumptionRate = data.FueConsumptionValue;

            fuelTank.UpdateFuelTank(data.FuelTankCapValue, fuel);

            //Debug.Log("inside in the if statement");
            //Debug.Log("current fuel amount " + fuel + " enemyData: " + data.FuelAmountValue);
            //Debug.Log("consumption rate " + fuelConsumptionRate + " enemyData: " + data.FueConsumptionValue);
            //Debug.Log("max fuel amount " + fuelTank.fuelBar.maxValue + " enemyData: " + data.FuelTankCapValue);
            //Debug.Log("max fuel amount " + fuelTank.fuelBar.maxValue + " current fuel amount: " + fuel);

        }
        else
        {
            fuelTank.UpdateFuelTank(fuel, fuel);
            data.FuelTankCapValue = fuelTank.fuelBar.maxValue;
            data.FueConsumptionValue = fuelConsumptionRate;
            data.FuelAmountValue = fuel;
        }

    }

    void Update()
    {
        HandleInput();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        ApplyMovement();
        AimDirectionRotation();
    }

    private void HandleInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetMouseButtonDown(0) && canPlayerShoot) // left click
        {
            if (shieldActive)
                return;

            weapon.Fire();
        }
		
        if (Input.GetMouseButtonDown(1)) // right click
        {
            gun.SetActive(!gun.activeInHierarchy);
            shield.SetActive(!shield.activeInHierarchy);

            shieldActive = !shieldActive;
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
                
                data.FuelAmountValue = fuel;

            }
            else if (!useFuel && movementInput.sqrMagnitude > 0.1f)
            {
                rb.AddForce(movementInput.normalized * thrustForceWithoutFuel);
            }
        }

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
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
}