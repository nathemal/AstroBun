using UnityEngine;

public class FuelLootFollowPlayer : MonoBehaviour
{
    public Transform targetToMoveTowards;
    public int lootMovementSpeed;

    private void Update()
    {
        if(targetToMoveTowards != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetToMoveTowards.position, lootMovementSpeed * Time.deltaTime);
            
        }
    }
}
