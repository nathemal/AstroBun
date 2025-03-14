using UnityEngine;

public class BulletSettings : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public float damage;
    [Header("The value closer to 0, firing is much faster")]
    public float fireRate;
    public Rigidbody2D[] rbs;
    private Rigidbody2D rb;

   
    private Vector2 directionToTarget;
    private bool hasDirectionToTarget = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        Destroy(gameObject, lifeSpan); 
    }

    private void Update()
    {
        if(hasDirectionToTarget && directionToTarget != Vector2.zero)
        {
            MoveToTarget();
        }
        
    }
    public void SetDirection(Vector2 newDirection)
    {
        directionToTarget = newDirection;
        hasDirectionToTarget = true;
    }

    private void MoveToTarget()
    {
        if (!hasDirectionToTarget || rbs == null) { return; }

        if (hasDirectionToTarget && rbs != null)
        {
            foreach (var bullet in rbs)
            {
                if (bullet != null)
                {
                    bullet.linearVelocity = directionToTarget.normalized * speed;
                }
            }
        }
    }
}