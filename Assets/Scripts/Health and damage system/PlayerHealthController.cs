using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealthController: MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Healthbar Healthbar;
    public UnityEvent onDeath;

    public GameObject deathMenu;

    [Header("For Saving Data")]
    [SerializeField] private PlayerData data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        //Load data
        //if (data.isNewGame)
        //{
        //    data.UpdateHealthInFirstScene(this);
        //}
        //if (data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == ""))
        //{
        //    data.UpdateHealthInNextScene(this);
        //}

    }

    public void TakeDamage(float damage) 
    {
        if (currentHealth <= 0)
        {
            deathMenu.SetActive(true);
            return;
        }

        currentHealth -= damage;

        if(currentHealth >= 0)
        {
            data.HealthValue = currentHealth;
        }
        //data.HealthValue = currentHealth;

        Healthbar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            deathMenu.SetActive(true);

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