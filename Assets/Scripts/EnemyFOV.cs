using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFOV : MonoBehaviour
{
    public GameObject player;
    public float radiusToNotice = 5.0f;
    [Range(1, 360)] public float FOVangle = 45f; //Field of view, how much of the enemy can see in front of them

    public LayerMask targetLayer;
    public LayerMask interferenceLayer;

    public bool canSeePlayer { get; private set; }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    //to control how often the signal is send to check if the player is seen
    private IEnumerator FOVCheck()
    {
        WaitForSeconds toWait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return toWait;
            CheckIfPlayerIsInFOV();
        }
    }


    private void CheckIfPlayerIsInFOV()
    {
        //check if any colliders exist on target layer
        Collider2D[] checkRange = Physics2D.OverlapCircleAll(transform.position, radiusToNotice, targetLayer);

        if (checkRange.Length > 0)
        {
            //it means the target is in range of the circle
            //grab first thing that enemy saw
            Transform target = checkRange[0].transform;
            //get direction to that target from enemy position
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < FOVangle)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, interferenceLayer))
                {
                    canSeePlayer = true;
                    Debug.Log("Player is seen");

                }
                else
                {
                    canSeePlayer = false;
                    Debug.Log("AFTER checking raycast: Player is not seen");
                }

            }
            else
            {
                canSeePlayer = false;
                Debug.Log("AFTER checking angle: Player is not seen");
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            Debug.Log("AFTER: checking the length: Player is not seen");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radiusToNotice);

        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -FOVangle);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, FOVangle);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radiusToNotice);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radiusToNotice);

        if(canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }


    private Vector2 DirectionFromAngle(float eulerY, float angleInDegree)
    {
        angleInDegree += eulerY;
        return new Vector2(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }

}
