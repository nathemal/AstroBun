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
            handlePlayerCollision(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            handleEnemyCollision(collision.gameObject);
        }
        if (collision.gameObject.tag == "Shield")
        {
            handleShieldCollision();
        }
    }

    private void handlePlayerCollision(GameObject entity)
    {
        var playerHealth = entity.GetComponent<PlayerHealthController>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(bullet.damage);
        }
       
        Destroy(gameObject);
    }

    private void handleEnemyCollision(GameObject entity)
    {
        var enemyHealth = entity.GetComponent<EnemyHealthController>(); //W hy IT IS SEPARATE? BECAUSE THIS CONTROLLER DOESN'T HAVE HEALTH BAR ELEMENT IN IT

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(bullet.damage);
        }

        Destroy(gameObject);
    }

    private void handleShieldCollision()
    {
        Destroy(gameObject);
    }
}