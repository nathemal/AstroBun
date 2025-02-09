using UnityEngine;

public class EnemyChase : MonoBehaviour
{

    //public WeaponBehaviour enemyGun;
    private GameObject player;
    public float speed;
    public float distanceToNoticeThePlayer;
    private float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
   
    }

    // Update is called once per frame
    void Update()
    {
       ChasePlayer();
        
    }


    public void ChasePlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if(distance < distanceToNoticeThePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToNoticeThePlayer);
    }

    //issue - when the player movement is fixed, the movement of the enemies needs to be fixed, because there are instances when they collide overlap each other





}
