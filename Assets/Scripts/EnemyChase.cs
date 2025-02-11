using UnityEngine;

public class EnemyChase : MonoBehaviour
{

    //public WeaponBehaviour enemyGun;
    //public EnemyFOV enemyFOVScript;
    private GameObject player;
    public float speed;
    private float distance;
    public float distanceToNoticePlayer;

    //public LayerMask targetLayer;
    //public LayerMask interferenceLayer;

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
        //1)
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceToNoticePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        }

        //------------
        //Attempts to improve it:
        //2)
        //Collider2D[] checkRange = Physics2D.OverlapCircleAll(transform.position, distanceToNoticePlayer, targetLayer);

        //if (checkRange.Length > 0)
        //{
        //    //it means the target is in range of the circle
        //    //grab first thing that enemy saw
        //    Transform target = checkRange[0].transform;
        //    //get direction to that target from enemy position
        //    Vector2 directionToTarget = (target.position - transform.position).normalized;

        //    float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        //    float distanceToTarget = Vector2.Distance(transform.position, target.position);


        //    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, interferenceLayer))
        //    {
        //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //    }
        //}


        //----
        //3)
        //if (enemyFOVScript.canSeePlayer())
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //    //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //}



        //---
        //4)
        //if (enemyFOVScript.canSeePlayer) //in stright line
        //{
        //    // Move towards the player
        //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        //    
        //    Vector2 playerForwardDirection = player.transform.up;

        //    // Calculate angle between enemy and player's facing direction
        //    float angleToRotate = Mathf.Atan2(playerForwardDirection.y, playerForwardDirection.x) * Mathf.Rad2Deg;

        //    // Rotate enemy smoothly to match player's rotation
        //    float rotationSpeed = 5f; // Adjust for smoother rotation
        //    Quaternion targetRotation = Quaternion.Euler(0, 0, angleToRotate);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //}
    }

    //issue - when the player movement is fixed, the movement of the enemies needs to be fixed, because there are instances when they collide overlap each other

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToNoticePlayer);
    }
}
