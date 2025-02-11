using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D rb;
    public float lifeSpan;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);

        Destroy(gameObject, lifeSpan);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
