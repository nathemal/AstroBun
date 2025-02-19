using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrustForce = 10f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 200f;
    public bool useFuel = true;
    public float fuel = 100f;
    public float fuelConsumptionRate = 10f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (movementInput.sqrMagnitude > 0.1f)
        {
            //REASON: I commented this out because the program is confused - doesnt understand in which way the rigid body needs to rotate,  because I use same rigid body for aiming.
            //I tried to add additional rigid body to gun, but when I playtested, it detached from the main body. I don't know if you want to rotate the main body differenty from the gun 'body' 

            //Tried to work on different values for the player movemnt but it is really unresponsive, feels like you skate on ice, it is really annoying.
           
            //float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

        }
    }

    void ApplyMovement()
    {
        if (fuel > 0 || !useFuel) // Only move if fuel is available or fuel usage is off
        {
            rb.AddForce(movementInput.normalized * thrustForce);
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

            if (useFuel && movementInput.sqrMagnitude > 0.1f)
            {
                fuel -= fuelConsumptionRate * Time.deltaTime;
                fuel = Mathf.Max(fuel, 0);
            }
        }
    }

    // Call this function to refuel the spaceship
    public void Refuel(float amount)
    {
        fuel += amount;
        fuel = Mathf.Min(fuel, 100f); // Cap fuel at max
    }

    //Kamile added this
    private void AimDirectionRotation()
    {
        //rotate the player/gun with the mouse
        Vector3 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
       
    }
}