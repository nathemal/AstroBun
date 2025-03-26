using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private GameObject player;
    private float distance;

    public EnemyAttack enemyAttackScript;
    public float distanceToNoticePlayer;
    public float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceToNoticePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            enemyAttackScript.Fire(player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToNoticePlayer);
    }
}
    