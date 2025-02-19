using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private BulletSettings bullet; 

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

        bool isPlayer = collision.gameObject.tag == "Player";

        if (isPlayer)
        {
            HandleCollision(collision.gameObject, isPlayer);

        }
        else if (!isPlayer && collision.gameObject.tag == "Enemy")
        {
            HandleCollision(collision.gameObject, !isPlayer);
        }

    }

    private void HandleCollision(GameObject entity, bool isPlayer)
    {
        var healthController = entity.gameObject.GetComponent<HealthController>();

        if( healthController == null ) { return; }

        if(isPlayer)
        {
            healthController.PlayerTakeDamage(bullet.damage);
        }
        else
        {
            healthController.EnemyTakeDamage(bullet.damage);
        }
        

        if (healthController.CheckIfEntityIsAlive() == false)
        {
            Destroy(entity);
        }

        //Destroy(gameObject); //destroy bullet
        gameObject.SetActive(false); //TO DO: ASK SOMEONE FOR HELP TO FIX THIS. WHEN I ISE SETACTIVE(FALSE) I DON'T HAVE ERROR, BUT ONCE I WANT TO DESTROY BULLET I HAVE ERROR IN BULLET SETTINGS BECAUSE OF MOVETOTARGET()
    }
}