using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private BulletSettings bullet;
    //public bool isDestroyed;

    /*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //for reusability
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    */

    private void Start()
    {
        bullet = GetComponentInParent<BulletSettings>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //for reusability
        if (bullet == null) { return; }

        if (collision.gameObject.tag == "Player")
        {
            HandlePlayerCollision(collision.gameObject);

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            HandleEnemyCollision(collision.gameObject);
        }

    }

    private void HandlePlayerCollision(GameObject entity)
    {
        var playerHealth = entity.GetComponent<PlayerHealthController>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(bullet.damage);

            //if (!playerHealth.CheckIfEntityIsAlive())
            //{
            //    Destroy(entity);
            //}
        }
       
        Destroy(gameObject);
    }


    private void HandleEnemyCollision(GameObject entity)
    {
        var enemyHealth = entity.GetComponent<EnemyHealthController>(); //W hy IT IS SEPARATE? BECAUSE THIS CONTROLLER DOESN'T HAVE HEALTH BAR ELEMENT IN IT
        //isDestroyed = false;

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(bullet.damage);

            //if (!enemyHealth.CheckIfEntityIsAlive())
            //{
            //    Destroy(entity); 
            //}
        }

        Destroy(gameObject);
    }
}