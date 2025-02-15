using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OrbitController : MonoBehaviour
{
    public float orbitSpeed = 3f; // Default orbit speed
    public float boostMultiplier = 1f; // Speed increase per second when orbiting (on spance press)
    public float launchMultiplier = 5f; 
    public float maxOrbitSpeed = 10f; 
    public float orbitRadius = 1.5f; // Distance from planet center of gravity
    private bool reverseOrbit = false; 
    private Transform currentPlanet;
    public float launchDecayRate = 2f; 

    private Rigidbody2D rb;
    private bool isOrbiting = false;
    private bool isDecaying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Planet"))
        {
            if (currentPlanet == other.transform) return;
            currentPlanet = other.transform;
            isOrbiting = true;
            isDecaying = false;

            // Get direction to the new planet
            Vector2 direction = (transform.position - currentPlanet.position).normalized;
            Vector2 tangent = reverseOrbit ? new Vector2(direction.y, -direction.x) : new Vector2(-direction.y, direction.x);

            // Reset velocity for new orbit
            orbitSpeed = 3f; // Reset orbit speed to default
            rb.linearVelocity = tangent * orbitSpeed;

            Debug.Log($"Player entered orbit around {currentPlanet.name}");
        }
    }

    void Update()
    {
        if (isOrbiting && currentPlanet != null)
        {
            Vector2 direction = (transform.position - currentPlanet.position).normalized;
            Vector2 targetPosition = (Vector2)currentPlanet.position + direction * orbitRadius;
            transform.position = targetPosition;

            // Reverse direction if p is pressed
            if (Input.GetKeyDown(KeyCode.P))
            {
                reverseOrbit = !reverseOrbit;
                Debug.Log($"Orbit direction reversed: {reverseOrbit}");
            }
            Vector2 tangent = reverseOrbit ? new Vector2(direction.y, -direction.x) : new Vector2(-direction.y, direction.x);

            // Apply orbital velocity
            rb.linearVelocity = tangent * orbitSpeed;

            // Boost orbit speed while holding space
            if (Input.GetKey(KeyCode.Space))
            {
                orbitSpeed += Time.deltaTime * boostMultiplier;
                orbitSpeed = Mathf.Min(orbitSpeed, maxOrbitSpeed);
                Debug.Log($"Boosting orbit speed: {orbitSpeed}");
            }

            // Launch player when space is released
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isOrbiting = false;
                isDecaying = true;
                Vector2 launchDirection = tangent.normalized;
                float launchForce = Mathf.Max(orbitSpeed * launchMultiplier);
                rb.linearVelocity = launchDirection * launchForce;
                StartCoroutine(DecaySpeed());
                Debug.Log($"Player launched from {currentPlanet.name} with velocity {rb.linearVelocity}");
            }

            // Debug to check if speed cap is working
            if (orbitSpeed >= maxOrbitSpeed) { Debug.Log("Max orbit speed reached!"); }
        }
    }


    IEnumerator DecaySpeed()
    {
        while (isDecaying && rb.linearVelocity.magnitude > maxOrbitSpeed)
        {
            rb.linearVelocity *= (1 - launchDecayRate * Time.deltaTime);
            Debug.Log($"Reducing speed: {rb.linearVelocity.magnitude}");
            yield return null;
        }
        isDecaying = false;
        Debug.Log("Speed decay complete");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Planet"))
        {
            isOrbiting = false;
            Debug.Log($"Player left orbit around {currentPlanet.name}");
            currentPlanet = null;
        }
    }
}



