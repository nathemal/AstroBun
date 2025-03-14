using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealthController: MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Healthbar Healthbar;
    public UnityEvent onDeath;

    [Header("For Saving Data")]
    [SerializeField] private PlayerData data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        //Load data


        if (data.isNewGame)
        {
            UpdateHealthInFirstScene();
        }
        else if (data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == ""))
        {
            UpdateHealthInNextScene();
        }
        
    }

    private void UpdateHealthInNextScene()
    {
        currentHealth = data.HealthValue;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void UpdateHealthInFirstScene()
    {
        currentHealth = maxHealth;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
        data.HealthValue = currentHealth;
    }


    public void TakeDamage(float damage) 
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddHealth(float healAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, Healthbar.healthSlider.maxValue); //cap heal according the heal bar capacity
      
        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }
}