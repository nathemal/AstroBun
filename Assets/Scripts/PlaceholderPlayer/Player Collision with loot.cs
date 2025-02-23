using UnityEngine;

public class PlayerCollisionwithloot : MonoBehaviour
{
    public FuelPowerUpSettings fuelPoerUp;
    private PlayerMovement fuelTank;
    void Start()
    {
        fuelTank = GetComponent<PlayerMovement>();
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
