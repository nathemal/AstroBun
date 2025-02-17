using UnityEngine;

public class EnemyRoaming : MonoBehaviour
{
    [Header("Bottom left corner of the area")]
    [SerializeField]
    Vector2 minBounds; 
    [SerializeField]
    [Header("Top right corner of the area")]
    Vector2 maxBounds; 
    [SerializeField]
    float speed;
    [Header("How close the enemy needs to be to the waipoint " +
            "to set new destinatio to new point")]
    [SerializeField]
    float range;
    [Header("How far the enemy can roam")]
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float rotationSpeed;

    Vector2 wayPoint;

    void Start()
    {
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }

        if (wayPoint != Vector2.zero)
        {
            SetRotationDirection();
        }
    }

    private void SetNewDestination()
    {
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        wayPoint = new Vector2(randomX, randomY);
        // Debug.Log($"New WayPoint Set: {wayPoint}");
    }

    private void SetRotationDirection()
    {
        // Calculate the direction vector from the current position to the waypoint
        Vector2 direction = wayPoint - (Vector2)transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion toRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
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
            // Reverse direction and set a new destination
            Vector2 collisionNormal = (Vector2)transform.position - collision.ClosestPoint(transform.position);
            wayPoint = (Vector2)transform.position + collisionNormal.normalized * range;

            wayPoint = new Vector2(
                Mathf.Clamp(wayPoint.x, minBounds.x + 0.1f, maxBounds.x - 0.1f),
                Mathf.Clamp(wayPoint.y, minBounds.y + 0.1f, maxBounds.y - 0.1f)
            );
        }
    }
}
