using UnityEngine;

public class FuelPickUp : MonoBehaviour
{
    public FuelPowerUpSettings fuelLootData; //stores all fuel power up data in here
    public FuelLootFollowPlayer fuelMovement;
    public void DropLoot(Vector3 enemyPosition, Transform playerTransform)
    {
        transform.position = enemyPosition;
        fuelMovement.targetToMoveTowards = playerTransform;
    }
}