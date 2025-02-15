using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public float damage;
    //public float cooldown;
    [Header("The value closer to 0, firing is much faster")]
    public float fireRate;
    public Rigidbody2D[] rbs;
    private Rigidbody2D rb;
    GameObject target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        Destroy(gameObject, lifeSpan);

        foreach(var bullet in rbs)
        {
            Vector2 directionToPlayer = (target.transform.position - transform.position).normalized * speed;
            bullet.linearVelocity = new Vector2(directionToPlayer.x, directionToPlayer.y);
        }
    }
}
