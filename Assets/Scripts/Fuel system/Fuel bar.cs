using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    public Slider fuelBar;
    public TextMeshProUGUI fuelText;

    public void UpdateFuelTank(float amount)
    {
        fuelBar.value = amount;
        UpdateFuelText((int)fuelBar.maxValue, amount);
    }

    public void SetMaxFuelTank(float amount)
    {
        fuelBar.maxValue = amount;
        fuelBar.value = amount;
        UpdateFuelText((int)amount, amount);
    }


    public void UpdateFuelText(int maxCapacity, float currentAmount)
    {
        int currentValue = (int) currentAmount;

        fuelText.text = " " + maxCapacity.ToString() + "/" + " " + currentValue.ToString();
        Debug.Log("fuel amount AFTER updating inside of function: " + fuelText.text);
    }
}