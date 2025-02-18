using UnityEngine;

public class BulletCollision : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D collision)
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
}