using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    public Slider fuelBar;
    
    public void UpdateFuelTank(float amount)
    {
        fuelBar.value = amount;
    }

    public void SetMaxFuelTank(float amount)
    {
        fuelBar.maxValue = amount;
        fuelBar.value = amount;
    }
}