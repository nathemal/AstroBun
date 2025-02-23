using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void HandleInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.F))
            useFuel = !useFuel;
        
        // TODO: Add a way to toggle useFuel in game here
    }

    void ApplyMovement()
    {
        if (fuel <= 0) 
            useFuel = false;

        if (movementInput.sqrMagnitude > 0.1f) // Moved this function here from Update to avoid tying any of the physics to fps
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
        }

        if (useFuel) // If fuel 
        {
            rb.AddForce(movementInput.normalized * thrustForceWithFuel);

            if (movementInput.sqrMagnitude > 0.1f)
            {
                fuel -= fuelConsumptionRate * Time.deltaTime;
                fuel = Mathf.Max(fuel, 0);
            }
        } else if (!useFuel)
        {
            rb.AddForce(movementInput.normalized * thrustForceWithoutFuel);
        }

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    // Call this function to refuel the spaceship
    public void Refuel(float amount)
    {
        fuel += amount;
        fuel = Mathf.Min(fuel, 100f); // Cap fuel at max
    }
}