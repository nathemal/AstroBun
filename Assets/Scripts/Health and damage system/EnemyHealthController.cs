using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private ChangeEnemyColor enemyColor;
    public int worthMoney;
    [Header("To destroy enemy")]
    public UnityEvent<int> onDeath;

    [Header("For Fuel Loot")]
    public GameObject fuelLootPrefab;
    //private Transform playerPosition;

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

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) 
            return;

        currentHealth -= damage;

        if(enemyColor != null)
        {
            enemyColor.ChangeSpriteColor();
        }

        if (currentHealth <= 0)
        {
            DropTheLoot();

            onDeath.Invoke(worthMoney); // Notify listeners that the enemy is dead
            Destroy(gameObject);
        }
    }

    public float CalculatehealthProcentage()
    {
        float procentage =  (currentHealth /  maxHealth) * 100.0f;
        
        return procentage;
    }

    private void DropTheLoot()
    {
        Vector3 enemyPosition = transform.position;

        //------------FOR TESTING 100 PROC DROP CHANCE----------
        GameObject fuelLootInstance = Instantiate(fuelLootPrefab, enemyPosition, Quaternion.identity);

        FuelPickUp loot = fuelLootInstance.GetComponent<FuelPickUp>();
        loot.DropLoot(enemyPosition);

        //if (CanLootbeDroped())
        //{
        //    GameObject fuelLootInstance = Instantiate(fuelLootPrefab, enemyPosition, Quaternion.identity);

        //    FuelPickUp loot = fuelLootInstance.GetComponent<FuelPickUp>();
        //    loot.DropLoot(enemyPosition);
        //}
    }

    private bool CanLootbeDroped()
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