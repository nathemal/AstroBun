using UnityEngine;

public class EnemyRoaming : MonoBehaviour
{
    // Roaming bounds will be calculated at runtime based on the enemy's spawn point.
    private Vector2 minBounds;
    private Vector2 maxBounds;

    [Header("Roaming Settings")]
    [SerializeField] float speed = 2f;              // Movement speed
    [SerializeField] float range = 0.5f;            // How close enemy must be to waypoint to pick a new one
    [SerializeField] float rotationSpeed = 180f;    // How fast the enemy rotates toward the waypoint

    private Vector2 wayPoint;

    void Start()
    {
        // Set the roaming area to be a 50x50 area centered on the enemy's spawn position.
        Vector2 spawnPoint = transform.position;
        minBounds = spawnPoint - new Vector2(25, 25);
        maxBounds = spawnPoint + new Vector2(25, 25);

        SetNewDestination();
    }

    void Update()
    {
        // Move toward the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);

        // If the enemy is close enough to the waypoint, choose a new one.
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }

        // Rotate smoothly to face the waypoint
        if (wayPoint != Vector2.zero)
        {
            SetRotationDirection();
        }
    }

    private void SetNewDestination()
    {
        // Choose a random point within the roaming area (50x50 centered on the spawn point)
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        wayPoint = new Vector2(randomX, randomY);
    }

    private void SetRotationDirection()
    {
        Vector2 direction = wayPoint - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        // Draw the roaming area if the min/max bounds are set (visible in Play mode)
        Gizmos.color = Color.green;
        Vector3 bottomLeft = new Vector3(minBounds.x, minBounds.y, 0);
        Vector3 topRight = new Vector3(maxBounds.x, maxBounds.y, 0);
        Vector3 topLeft = new Vector3(minBounds.x, maxBounds.y, 0);
        Vector3 bottomRight = new Vector3(maxBounds.x, minBounds.y, 0);

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            // If the enemy collides with a boundary, adjust its waypoint to remain within the roam area.
            Vector2 collisionNormal = (Vector2)transform.position - collision.ClosestPoint(transform.position);
            wayPoint = (Vector2)transform.position + collisionNormal.normalized * range;

            wayPoint = new Vector2(
                Mathf.Clamp(wayPoint.x, minBounds.x + 0.1f, maxBounds.x - 0.1f),
                Mathf.Clamp(wayPoint.y, minBounds.y + 0.1f, maxBounds.y - 0.1f)
            );
        }
    }
}
