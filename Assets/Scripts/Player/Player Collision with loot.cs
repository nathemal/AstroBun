using UnityEngine;

public class PlayerCollisionWithLoot : MonoBehaviour
{
    private PlayerController fuelTank;
    private PlayerHealthController healthBar;
    void Start()
    {
        fuelTank = GetComponent<PlayerController>();
        healthBar = GetComponent<PlayerHealthController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FuelPickup")
        {
            CollisionWithFuelLoot(collision);
        }
        else if (collision.tag == "HealthPickup")
        {
            CollisionWithHealthLoot(collision);
        }

    }

    private void CollisionWithFuelLoot(Collider2D collision)
    {
        FuelPickUp fuelObject = collision.GetComponent<FuelPickUp>(); 

        if (fuelObject != null && fuelObject.fuelLootData != null)
        {
            fuelTank.Refuel(fuelObject.fuelLootData.additionalFuelAmount);

        }

        Destroy(collision.gameObject); // Destroy the loot
    }


    private void CollisionWithHealthLoot(Collider2D collision)
    {
        HealPickUp healObject = collision.GetComponent<HealPickUp>(); 

        if (healObject != null && healObject.healLootData != null)
        {
            healthBar.AddHealth(healObject.healLootData.healAmount);
        }

        Destroy(collision.gameObject);
    }
}