using UnityEngine;

public class PlayerCollisionwithloot : MonoBehaviour
{
    public FuelPowerUpSettings fuelPoerUp;
    private PlayerMovement fuelTank;
    void Start()
    {
        fuelTank = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // This method deals with collisions with colliders that are set to trigger
    {
        if (collision.tag == "FuelPickup") // Weapon pickup collision
        {
            fuelTank.Refuel(fuelPoerUp.additionalFuelAmount);

            Destroy(collision.gameObject); // Destroy the weapon pickup
        }
    }


}
