using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerHealthController: MonoBehaviour
{
    //[HideInInspector] public Vector3 startPosition = new Vector3 (15.3f, -19.5f, 0.0f);
    //public GameObject player;
    public float maxHealth;
    public float currentHealth;
    public Healthbar Healthbar;
    public UnityEvent onDeath;

    public GameObject deathMenu;

    [Header("For Saving Data")]
    [SerializeField] private PlayerData data;

    [Header("Take Damage Post Processing Settings")]
    public Volume takeDamageVolume; // Needs a reference to the post processing volume with the take damage profile
    bool takeDamage; // Bool that is set true when the player takes damage, and is set false when the post processing fading is done
    bool takeDamageFadeIn; // Bool that checks if the post processing is fading in (true) or out (false)
    public float takeDamageFadeInTime; // The amount of time it takes for the post processing to fade in
    public float takeDamageFadeOutTime; // The amount of time it takes for the post processing to fade out
    float takeDamageTimer; // The temp timer value to do the fade timer
    float startWeight;

    [Header("Low Health Post Processing Settings")]
    public Volume lowHealthVolume;
    [Range(0f, 1f)]
    public float showLowHealthVolumePercentageThreshold;

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

    private void Update()
    {
        float healthRatio = (1f - (currentHealth / maxHealth)); // Update the current health ratio (1 means full health, 0 is no health)
        if (healthRatio >= showLowHealthVolumePercentageThreshold) // Check to see if the threshold for displaying the low health post processing is met
        {
            float weight = (healthRatio - showLowHealthVolumePercentageThreshold) / (1 - showLowHealthVolumePercentageThreshold); // Calculate the weight of the low health volume
            weight += 0.5f * Mathf.PingPong(Time.time, 0.5f * (1.2f - weight)); // Add some flashing to the low health volume weight
            lowHealthVolume.weight = Mathf.SmoothStep(0, 1, weight); // Set the low health volume in a lerp from 0 to 1

            if (takeDamage)
            {
                ChangeVolumeWhenTakingDamage();
            }
        }
    }

    public void TakeDamage(float damage) 
    {
        if (currentHealth <= 0)
        {
            deathMenu.SetActive(true);
            //player.SetActive(false);
            //player.transform.position = startPosition;

            return;
        }

        currentHealth -= damage;

        if(currentHealth >= 0)
        {
            data.HealthValue = currentHealth;
        }
        //data.HealthValue = currentHealth;

        takeDamage = true;
        ChangeVolumeWhenTakingDamage();

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

        ChangeVolumeBasedOnHeal();

        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }



    private void ChangeVolumeBasedOnHeal()
    {
        takeDamageTimer = 0;
        takeDamageFadeIn = true;

        startWeight = takeDamageVolume.weight; // Store the current weight of the volume so we can revert back to this
        takeDamage = true; // Start the post processing fading in Update
    }


    private void ChangeVolumeWhenTakingDamage()
    {

        if (takeDamage) // This is true when the player takes damage
        {
            Debug.Log("in if statement in taking damage volume");
            takeDamageTimer += Time.deltaTime; // Start the timer

            if (takeDamageFadeIn) // Is true when the post processing is fading in
            {
                takeDamageVolume.weight = Mathf.SmoothStep(takeDamageVolume.weight, 1, takeDamageTimer / takeDamageFadeInTime); // Set the post processing weight from 0 to 1

                Debug.Log("in if statement in fade in - taking damage volume");
                if (takeDamageTimer >= takeDamageFadeInTime) // Check when the fade in is over
                {
                    takeDamageVolume.weight = 1; // Set the weight to 1 for good measure
                    takeDamageFadeIn = false; // Set to false to start the fade out
                    takeDamageTimer = 0; // Reset timer
                }
            }
            else // When takeDamageFadeIn is false, for the fade out
            {
                Debug.Log("In else statement");
                takeDamageVolume.weight = Mathf.SmoothStep(takeDamageVolume.weight, startWeight, takeDamageTimer / takeDamageFadeOutTime); // Fade out the post processing, from 1 to 0

                if (takeDamageTimer >= takeDamageFadeOutTime) // Check when the timer is done
                {
                    takeDamageVolume.weight = 0; // Set weight to 0 for good measure
                    takeDamage = false; // Stop the taking damage post processing since it has now both faded in and out again
                }
            }
            Debug.Log("at the end of function");
        }
    }
}