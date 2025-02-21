using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private ChangeEnemyColor enemyColor;
    public int worthMoney;
    public UnityEvent<int> onDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyColor = GetComponent<ChangeEnemyColor>();

        EarnMoney currencyManager = FindAnyObjectByType<EarnMoney>();
        if (currencyManager != null)
        {
            onDeath.AddListener(currencyManager.AddMoney);
        }
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

        if (currentHealth <= 0)
        {
            onDeath.Invoke(worthMoney); // Notify listeners that the enemy is dead
            Destroy(gameObject);
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



