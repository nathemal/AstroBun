using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OldPlayerMovement : MonoBehaviour
{
    public float thrustForce = 10f;
    public float maxSpeed = 5f;
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

        if (movementInput.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
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
}