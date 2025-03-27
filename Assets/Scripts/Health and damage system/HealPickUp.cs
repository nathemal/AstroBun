using UnityEngine;

public class HealPickUp : MonoBehaviour
{
    public HealPowerUpSettings healLootData; //stores all heal power up data in here
    public LootFollowPlayer fuelMovement;
    public void DropLoot(Vector3 enemyPosition)
    {
        transform.position = enemyPosition;
    }
}
