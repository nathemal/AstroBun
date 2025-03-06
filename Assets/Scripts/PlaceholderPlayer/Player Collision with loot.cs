using UnityEngine;

public class PlayerCollisionwithloot : MonoBehaviour
{
    public FuelPowerUpSettings fuelPoerUp;
    private PlayerController fuelTank;
    void Start()
    {
        fuelTank = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FuelPickup")
        {
            fuelTank.Refuel(fuelPoerUp.additionalFuelAmount);

            Destroy(collision.gameObject); // Destroy the weapon pickup
        }
    }
}