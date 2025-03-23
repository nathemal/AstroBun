using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckMapBoundaries : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider2D; //for colliding with walls
    private PolygonCollider2D polygonCollider2D; //for the taking damage
    public float radiusOfRayCast;
    public LayerMask playerLayer;
    public LayerMask obsticleLayer;
    
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        capsuleCollider2D.enabled = false;
        polygonCollider2D.enabled = true;

        StartCoroutine(RaycastingWall());
    }

    private IEnumerator RaycastingWall()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        while(true)
        {
            yield return waitTime;
            StopPlayerMovingThroughWalls();
        }
    }

    private bool CheckIfThePlayerHittedWall()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radiusOfRayCast, playerLayer);
        
        if(rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;

            Vector2 directionToWall = (target.position - transform.position).normalized;

            float distanceToWall = Vector2.Distance(transform.position, target.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToWall, distanceToWall, obsticleLayer);
            
            if (hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    private void StopPlayerMovingThroughWalls()
    {
        if(CheckIfThePlayerHittedWall())
        {
            capsuleCollider2D.enabled = true;
            polygonCollider2D.enabled = false;
        }
        else
        {
            polygonCollider2D.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfRayCast);
    }
}
