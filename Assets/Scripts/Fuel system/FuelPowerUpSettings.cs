using UnityEngine;

public class FuelPowerUpSettings : MonoBehaviour
{
    public float additionalFuelAmount;
    public Fuelbar fuelTank;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateFuelTank()
    {
        fuelTank.UpdateFuelTank(additionalFuelAmount);
    }

    public void SetActiveFuelPowerUp()
    {
        gameObject.SetActive(true);
    }
}