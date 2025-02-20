using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealthController: MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Healthbar Healthbar;
    //public UnityEvent OnDie; <- when is dead, stop moving, stop shooting (do I need this?)
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       currentHealth = maxHealth;
       Healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))//for testing
        {
            TakeDamage(10); 
        }
    }

    public void TakeDamage(float damage) 
    {
        if (currentHealth <= 0) { return; }

        currentHealth -= damage;
        Healthbar.UpdateHealthBar(currentHealth);

    }

    //if we want to have powerup as heal
    public void AddHealth(float healAmount)
    {
        if (currentHealth <= 0) { return; }

        currentHealth += healAmount;
        Healthbar.UpdateHealthBar(currentHealth);
    }


    public bool CheckIfEntityIsAlive()
    {
        return currentHealth > 0;
    }

}
