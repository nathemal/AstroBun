using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public float orbitSpeed = 2f; // Default orbit speed
    public float boostMultiplier = 5f; // Speed increase per second
    public float launchMultiplier = 1.5f; // Extra speed boost on launch
    public float maxOrbitSpeed = 10f; 
    public float orbitRadius = 2f; // Distance from planet center of gravity
    private bool reverseOrbit = false; 
    private Transform currentPlanet; 

    private Rigidbody2D rb;
    private bool isOrbiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters another planet's gravity field
        if (other.CompareTag("Planet"))
        {
            // Prevent the player from entering orbit twice with the same planet
            if (currentPlanet == other.transform) return;

            // Transfer to the new planet's orbit
            currentPlanet = other.transform;
            isOrbiting = true;
            orbitSpeed = 2f; // Reset or adjust speed when transferring

            Debug.Log($"Player entered orbit around {currentPlanet.name}");
        }
    }

    void Update()
    {
        if (isOrbiting && currentPlanet != null)
        {
            // Get direction from planet to player
            Vector2 direction = (transform.position - currentPlanet.position).normalized;

            // Calculate correct orbital position at fixed radius
            Vector2 targetPosition = (Vector2)currentPlanet.position + direction * orbitRadius;

            // Move player to that position
            transform.position = targetPosition;

            // Reverse direction if "P" key is pressed
            if (Input.GetKeyDown(KeyCode.P))
            {
                reverseOrbit = !reverseOrbit;
                Debug.Log($"Orbit direction reversed: {reverseOrbit}");
            }

            // Reverse the tangential direction based on reverseOrbit flag
            Vector2 tangent = reverseOrbit ? new Vector2(direction.y, -direction.x) : new Vector2(-direction.y, direction.x);

            // Apply orbital velocity
            rb.linearVelocity = tangent * orbitSpeed;

            // Boost orbit speed while holding space
            if (Input.GetKey(KeyCode.Space))
            {
                orbitSpeed += Time.deltaTime * boostMultiplier;
                orbitSpeed = Mathf.Min(orbitSpeed, maxOrbitSpeed); // Cap the speed
                Debug.Log($"Boosting orbit speed: {orbitSpeed}");
            }

            // Launch player when space is released
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isOrbiting = false;
                rb.linearVelocity *= launchMultiplier; // Add boost for launch

                Debug.Log($"Player launched from {currentPlanet.name} with velocity {rb.linearVelocity}");
            }

            // Debug to check if speed cap is working
            if (orbitSpeed >= maxOrbitSpeed)
            {
                Debug.Log("Max orbit speed reached!");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Exit the current planet�s orbit if the player leaves the planet�s gravity field
        if (other.CompareTag("Planet"))
        {
            isOrbiting = false;
            Debug.Log($"Player left orbit around {currentPlanet.name}");
            currentPlanet = null;
        }
    }
}
