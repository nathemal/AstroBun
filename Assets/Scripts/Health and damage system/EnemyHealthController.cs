using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private ChangeEnemyColor enemyColor;
    public int worthMoney;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyColor = GetComponent<ChangeEnemyColor>();
    }

    private void Update()
    {
        //FIX LATER
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) { return; }

        currentHealth -= damage;

        if(enemyColor != null)
        {
            enemyColor.ChangeSpriteColor();
        }

    }

    public bool CheckIfEntityIsAlive()
    {
        return currentHealth > 0;
    }

    public float CalculatehealthProcentage()
    {
        float procentage =  (currentHealth /  maxHealth) * 100.0f;
        
        return procentage;
    }
   
}



