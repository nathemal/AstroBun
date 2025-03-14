using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    public Slider fuelBar;
    public TextMeshProUGUI fuelText;

    public void UpdateFuelTank(float maxValue, float currentAmount)
    {
        fuelBar.maxValue = maxValue;
        fuelBar.value = currentAmount;
        UpdateFuelText((int)fuelBar.maxValue, currentAmount);
    }

    public void UpdateFuelText(int maxCapacity, float currentAmount)
    {
        int currentValue = (int) currentAmount;

        fuelText.text = " " + maxCapacity.ToString() + "/" + " " + currentValue.ToString();
       // Debug.Log("fuel amount AFTER updating inside of function: " + fuelText.text);
    }
}