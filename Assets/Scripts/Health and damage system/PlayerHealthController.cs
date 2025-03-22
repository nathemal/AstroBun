using Unity.Mathematics;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
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

    [Header("Take Damage Post Processing Settings")]
    public Volume takeDamageVolume;
    bool takeDamage;
    bool takeDamageFadeIn;
    public float takeDamageFadeInTime;
    public float takeDamageFadeOutTime; 
    float takeDamageTimer;
    float startWeight;

    [Header("Low Health Post Processing Settings")]
    public Volume lowHealthVolume;
    [Range(0f, 1f)]
    [Header("To see low health effects on sceen:")]
    public float showLowHealthVolumePercentageThreshold;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        lowHealthVolume.weight = 0;
        takeDamageVolume.weight = 0;
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
        currentHealth = Mathf.Max(currentHealth, 0);
        data.HealthValue = currentHealth;

        takeDamage = true;

        Healthbar.UpdateHealthBar(maxHealth, currentHealth);

        UpdatePostProcessingEffects();

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

        ChangeVolumeBasedOnHeal();

        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }



    private void UpdatePostProcessingEffects()
    {
        float healthRatio = (1f - (currentHealth / maxHealth)); // Update the current health ratio (1 means full health, 0 is no health)
        if (healthRatio >= showLowHealthVolumePercentageThreshold)
        {
            float weight = (healthRatio - showLowHealthVolumePercentageThreshold) / (1 - showLowHealthVolumePercentageThreshold); // Calculate the weight of the low health volume
            weight += 0.5f * Mathf.PingPong(Time.time, 0.5f * (1.2f - weight)); // Add some flashing
            lowHealthVolume.weight = Mathf.SmoothStep(0, 1, weight);

            ChangeVolumeWhenTakingDamage();
        }
    }

    private void ChangeVolumeBasedOnHeal()
    {
        takeDamageTimer = 0;
        takeDamageFadeIn = true;

        startWeight = takeDamageVolume.weight;
        takeDamage = true; // Start the post processing fading in Update
    }


    private void ChangeVolumeWhenTakingDamage()
    {
        if (!takeDamage) { return; } // This is true when the player takes damage
        
        takeDamageTimer += Time.deltaTime;

        if (takeDamageFadeIn)
        {
            takeDamageVolume.weight = Mathf.SmoothStep(takeDamageVolume.weight, 1, takeDamageTimer / takeDamageFadeInTime);

            if (takeDamageTimer >= takeDamageFadeInTime)
            {
                takeDamageVolume.weight = 1;
                takeDamageFadeIn = false;
                takeDamageTimer = 0;
            }
        }
        else
        {
            takeDamageVolume.weight = Mathf.SmoothStep(takeDamageVolume.weight, startWeight, takeDamageTimer / takeDamageFadeOutTime);

            if (takeDamageTimer >= takeDamageFadeOutTime)
            {
                takeDamageVolume.weight = 0;
                takeDamage = false;
            }
        }
    }
}