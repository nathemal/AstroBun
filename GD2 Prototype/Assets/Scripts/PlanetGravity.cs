using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravityStrength = 10f;

    void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Calculate direction to the planet
            Vector2 direction = (transform.position - other.transform.position).normalized;

            // Apply force towards the planet
            rb.AddForce(direction * gravityStrength);

            // Debugging info
            Debug.Log($"Applying gravity to {other.name} with force {direction * gravityStrength}");
        }
    }
}


