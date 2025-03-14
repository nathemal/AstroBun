using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

}