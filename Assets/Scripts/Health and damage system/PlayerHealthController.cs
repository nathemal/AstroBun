using Unity.Mathematics;
using UnityEditor.Rendering.LookDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerHealthController: MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Healthbar Healthbar;
    //public UnityEvent onDeath;
    private bool isDead = false;

    public GameObject deathMenu;

    [Header("For Saving Data")]
    public PlayerData data;

    private PlayerPostProcessing postProcessing;
    public bool takeDamage;
    private float lastDamageTime;
    private int activeHitsByBullets = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        postProcessing = GetComponent<PlayerPostProcessing>();

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
        if (isDead) return;

        currentHealth -= damage;
        
        data.HealthValue = currentHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            Healthbar.UpdateHealthBar(maxHealth, currentHealth);

            deathMenu.SetActive(true);
            Destroy(gameObject);
            return;
        }

        Healthbar.UpdateHealthBar(maxHealth, currentHealth);

        activeHitsByBullets++;
        takeDamage = true;
        lastDamageTime = Time.time;
        postProcessing.ChangeVolumeWhenTakingDamage();

        StartCoroutine(CheckIfStillBeingHit());
    }

    public void AddHealth(float healAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, Healthbar.healthSlider.maxValue); //cap heal according the heal bar capacity
        
		postProcessing.ChangeVolumeBasedOnHeal(currentHealth);

        data.HealthValue = currentHealth;
		
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }


    private IEnumerator CheckIfStillBeingHit()
    {
        yield return new WaitForSeconds(0.5f);

        activeHitsByBullets--;

        if (activeHitsByBullets <= 0)
        {
            takeDamage = false;
            postProcessing.ChangeVolumeWhenPlayerIsNotBeingHit();
        }
    }
}