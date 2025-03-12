using UnityEngine;

public class PlayerCollisionWithLoot : MonoBehaviour
{
    private PlayerController fuelTank;
    void Start()
    {
        fuelTank = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FuelPickup")
        {
            FuelPickUp fuelObject = collision.GetComponent<FuelPickUp>(); //search for fuelPickUp script
                                                                          //that is attached to the collided object
                                                                          //GetComponent would search for the script
                                                                          //that is attached to the player

            if(fuelObject != null && fuelObject.fuelLootData != null)
            {
                fuelTank.Refuel(fuelObject.fuelLootData.additionalFuelAmount);
                
            }

            Destroy(collision.gameObject); // Destroy the weapon pickup
        }

    }
}