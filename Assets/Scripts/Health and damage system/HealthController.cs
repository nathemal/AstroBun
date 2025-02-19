using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class HealthController: MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Healthbar Healthbar;
    //public UnityEvent OnDie; <- when is dead, stop moving, stop shooting (do I need this?)
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Healthbar.SetMaxHealth(maxHealth);
       currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))//for testing
        {
            PlayerTakeDamage(10); 
        }
    }

    public void TakeDamage(float damage) //for the enemy and the player
    {
        if (currentHealth <= 0) { return; }

        currentHealth -= damage;
        Healthbar.UpdateHealthBar(currentHealth);

    }

    public void PlayerTakeDamage(float damage)
    {
        if(currentHealth <= 0) { return; }

        currentHealth -= damage;
        Healthbar.UpdateHealthBar(currentHealth);

    }

    public void EnemyTakeDamage(float damage) //if the enemy doesn't have a health bar
    {
        if (currentHealth <= 0) { return; }

        currentHealth -= damage;
    }

    public bool CheckIfEntityIsAlive()
    {
        return currentHealth > 0;
    }

}
