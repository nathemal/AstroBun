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

        if (data != null && data.lastSceneName != currentSceneName)
        {
            currentHealth = data.HealthValue;

            Healthbar.healthSlider.value = currentHealth;
            Healthbar.healthSlider.maxValue = maxHealth;
            //Healthbar.UpdateHealthBar(currentHealth);
        }
        else
        {
            currentHealth = maxHealth;
            Healthbar.SetMaxHealth(maxHealth);

        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamage(float damage) 
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(currentHealth);

        if (currentHealth <= 0)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

    //if we want to have powerup as heal
    public void AddHealth(float healAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth += healAmount;

        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(currentHealth);
    }

}