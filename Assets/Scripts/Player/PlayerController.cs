using UnityEngine;
using System.Collections;

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

    //Dash
    public float dashForce = 50f;      
    public float dashCooldown = 0f;    
    public float dashDuration = 0.8f;  
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;

    private bool canPlayerShoot = true;

    public PlayerAttack weapon;
    public GameObject gun;
    public GameObject shield;
    public Fuelbar fuelTank;

    OrbitController orbitController;
    Rigidbody2D rb;
    Vector2 movementInput;
    Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orbitController = GetComponent<OrbitController>();

        fuelTank.SetMaxFuelTank(fuel);
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

       if (!isDashing) // Prevents movement from interfering during dash
        {
            ApplyMovement();
        }
    }

    private void HandleInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
        }

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
                fuelTank.UpdateFuelTank(fuel);
                fuel = Mathf.Max(fuel, 0);
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

        //Debug.Log("fuel amount AFTER: " + fuel);

        fuel = Mathf.Min(fuel, 100f); // Cap fuel at max
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

    private void Dash()
    {
        if (fuel >= dashForce) // Check if player has enough fuel
        {
            fuel -= dashForce; // Consume fuel
            fuelTank.UpdateFuelTank(fuel); // Update UI

            Vector2 dashDirection = rb.linearVelocity.normalized; // Get current movement direction
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse); // Apply dash force instantly

            Debug.Log("Player dashed!");
        }
        else
        {
            Debug.Log("Not enough fuel to dash!");
        }
    }


}