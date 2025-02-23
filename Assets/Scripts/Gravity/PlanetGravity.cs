using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    private Planet planet;
    private float gravityStrength;

    void Start()
    {
        planet = GetComponent<Planet>();
        if (planet != null && planet.planetData != null)
        {
            gravityStrength = planet.planetData.gravityStrength;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Ignoring collision with {other.name} (Enemy)");
            return;
        }

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction * gravityStrength);
        }
    }
}