using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float thrustForceWithFuel = 5f;
    public float thrustForceWithoutFuel = 2f;
    public float maxSpeed = 7f;
    public float rotationSpeed = 200f;
    public bool useFuel = true;
    public float fuel = 100f;
    public float fuelConsumptionRate = 10f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    Vector2 mousePosition;
    public Fuelbar fuelTank;
    private OrbitController orbitController;

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
    }

    void HandleInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (Input.GetKeyDown(KeyCode.F))
            useFuel = !useFuel;
    }

    void ApplyMovement()
    {
        if (fuel <= 0) 
            useFuel = false;

        if (movementInput.sqrMagnitude > 0.1f) // Moved this function here from Update to avoid tying any of the physics to fps
        {
            //REASON: I commented this out because the program is confused - doesnt understand in which way the rigid body needs to rotate,  because I use same rigid body for aiming.
            //I tried to add additional rigid body to gun, but when I playtested, it detached from the main body. I don't know if you want to rotate the main body differenty from the gun 'body' 

            //Tried to work on different values for the player movemnt but it is really unresponsive, feels like you skate on ice, it is really annoying.

            //float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
        }

        if (!orbitController.isOrbiting)
        {
            if (useFuel) 
            {
                fuel -= fuelConsumptionRate * Time.deltaTime;
                fuelTank.UpdateFuelTank(fuel);
                fuel = Mathf.Max(fuel, 0);
				
                rb.AddForce(movementInput.normalized * thrustForceWithFuel);

                /*if (movementInput.sqrMagnitude > 0.1f)
                {
                    fuel -= fuelConsumptionRate * Time.deltaTime;
                    fuel = Mathf.Max(fuel, 0);
                }*/
            } else if (!useFuel)
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
}