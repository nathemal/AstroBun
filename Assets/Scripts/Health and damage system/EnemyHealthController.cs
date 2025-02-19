using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

    }

    private void Update()
    {
        //FIX LATER
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) { return; }

        currentHealth -= damage;

    }

    public bool CheckIfEntityIsAlive()
    {
        return currentHealth > 0;
    }

    private void ChangeSpriteColor()
    {
        if(currentHealth == maxHealth)
        {
            //color this ?
        }

        if(currentHealth < 0) //equal to 50 procent
        {

        }

        if (currentHealth < 0) //less than 30 procent
        {

        }
    }

    private float CalculatehealthProcentage()
    {
        float procentage =  currentHealth /  maxHealth;
        
        return procentage;
    }
}
