using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private BulletSettings bullet;

    private void Start()
    {
        bullet = GetComponentInParent<BulletSettings>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bullet == null) 
            return;

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
        }
       
        Destroy(gameObject);
    }


    private void HandleEnemyCollision(GameObject entity)
    {
        var enemyHealth = entity.GetComponent<EnemyHealthController>(); //W hy IT IS SEPARATE? BECAUSE THIS CONTROLLER DOESN'T HAVE HEALTH BAR ELEMENT IN IT

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(bullet.damage);
        }

        Destroy(gameObject);
    }
}