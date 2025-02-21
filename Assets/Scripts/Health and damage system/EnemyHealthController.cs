using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private ChangeEnemyColor enemyColor;
    public int worthMoney;
    public UnityEvent<int> onDeath;
    public FuelPowerUpSettings fuelLoot;

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
            //calculate probability if that enemy can leave fuel loot
            fuelLoot.transform.position = transform.position;
            
            if(CanLootbeDroped())
            {
                fuelLoot.SetActiveFuelPowerUp();
            }
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
   

    public bool CanLootbeDroped()
    {
        float dropChance = 30f;
        float roll = Random.Range(0f, 100f);
        Debug.Log("Chance was " + roll);
        if (roll < dropChance)
        {
            Debug.Log("Loot was dropped");
            return true;
        }
        return false;
    }
}



