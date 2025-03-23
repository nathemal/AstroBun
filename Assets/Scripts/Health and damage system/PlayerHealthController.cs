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
    [SerializeField] private PlayerData data;

    private PlayerPostProcessing postProcessing;
    public bool takeDamage;
    //public CameraShake sceneCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        postProcessing = GetComponent<PlayerPostProcessing>();

        //sceneCamera = Camera.main.GetComponent<CameraShake>();

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
        //if (currentHealth <= 0)
        //{
        //    deathMenu.SetActive(true);
        //    return;
        //}
        if (isDead) return;

        currentHealth -= damage;
        
        data.HealthValue = currentHealth;


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            Healthbar.UpdateHealthBar(maxHealth, currentHealth);

            deathMenu.SetActive(true);

            //onDeath?.Invoke(); //I got stack overflow because this
            Destroy(gameObject);
            return;
        }


        Healthbar.UpdateHealthBar(maxHealth, currentHealth);

        takeDamage = true;
        
        //StartCoroutine(sceneCamera.ShakeCamera(sceneCamera.durationOfShake, sceneCamera.strenghtOfShake));

    }

    public void AddHealth(float healAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, Healthbar.healthSlider.maxValue); //cap heal according the heal bar capacity

        postProcessing.ChangeVolumeBasedOnHeal();

        data.HealthValue = currentHealth;
        Healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }
}