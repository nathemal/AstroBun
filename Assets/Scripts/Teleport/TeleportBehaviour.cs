using System.Collections.Generic;
using UnityEngine;

public class TeleportBehaviour : MonoBehaviour
{
    private HashSet<GameObject> recentlyTeleportedObjects = new HashSet<GameObject>();
    [SerializeField] private Transform destinationToBeTeleported;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destinationToBeTeleported == null || recentlyTeleportedObjects.Contains(collision.gameObject))
        {
            return;
        }

        //Prevent immediate infinite teleportation loops
        TeleportBehaviour targetTeleportation = destinationToBeTeleported.GetComponent<TeleportBehaviour>();
        if (targetTeleportation != null)
        {
            targetTeleportation.recentlyTeleportedObjects.Add(collision.gameObject);
        }

        collision.transform.position = destinationToBeTeleported.position;

        //flip the object
       Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.linearVelocity = -rb.linearVelocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       recentlyTeleportedObjects.Remove(collision.gameObject);
    }
}