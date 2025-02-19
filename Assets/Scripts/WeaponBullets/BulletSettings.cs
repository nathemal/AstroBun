using UnityEngine;

public class BulletSettings : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public float damage;
    //public float cooldown;
    [Header("The value closer to 0, firing is much faster")]
    public float fireRate;
    public Rigidbody2D[] rbs;
    private Rigidbody2D rb;


    //private GameObject target;
    //private bool hasTarget = false;

    private Vector2 directionToTarget;
    private bool hasDirectionToTarget = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifeSpan);
    }

    private void Update()
    {
        if(hasDirectionToTarget)
        {
            MoveToTarget();
        }
        
    }

    //public void SetTarget(GameObject newTarget)
    //{
    //    target = newTarget;
    //    hasTarget = true;
    //}

    public void SetDirection(Vector2 newDirection)
    {
        directionToTarget = newDirection;
        hasDirectionToTarget = true;
    }

    private void MoveToTarget()
    {
        //if (target != null && rbs != null && hasTarget)
        //{
        //    foreach (var bullet in rbs)
        //    {
        //        Vector2 directionToTarget = (target.transform.position - transform.position).normalized * speed;
        //        bullet.linearVelocity = new Vector2(directionToTarget.x, directionToTarget.y);
        //    }
        //}

        if(hasDirectionToTarget && rbs != null)
        {
            foreach (var bullet in rbs)
            {
                bullet.linearVelocity = directionToTarget * speed;
            }
        }


    }

    //TO DO: if the player deletes the enemy, you have error
}