using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private ChangeEnemyColor enemyColor;

    public int worthMoney;
    //public static float worthMoneyMultiplier = 1.0f; //no effect

    [Header("To destroy enemy")]
    public UnityEvent<int> onDeath;

    [Header("For Fuel Loot")]
    public GameObject fuelLootPrefab;
    public float dropChance = 30.0f;
    public static float dropChanceMultiplier = 1.0f; //no effect

    [Header("To save data")]
    public EnemyData data; 

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        
        if (data != null && data.lastSceneName != currentSceneName)
        {
            worthMoney = data.WorthMoneyValue;
            dropChance = data.FuelDropChanceValue;
            //Debug.Log("inside in the if statement");
            //Debug.Log("worth money " + worthMoney + " enemyData: " + data.WorthMoneyValue);
            //Debug.Log("drop chance " + dropChance + " enemyData: " + data.FuelDropChanceValue);
        }
       
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
            Debug.Log("Before invoking event enemy worth: " + worthMoney);

            //int money = GetModifiedWorthMoney();
            onDeath.Invoke(worthMoney); // Notify listeners that the enemy is dead
            Debug.Log("After invoking event enemy worth: " + worthMoney);
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
        //GameObject fuelLootInstance = Instantiate(fuelLootPrefab, enemyPosition, Quaternion.identity);

        //FuelPickUp loot = fuelLootInstance.GetComponent<FuelPickUp>();
        //loot.DropLoot(enemyPosition);

        if (CanLootbeDroped())
        {
            GameObject fuelLootInstance = Instantiate(fuelLootPrefab, enemyPosition, Quaternion.identity);

            FuelPickUp loot = fuelLootInstance.GetComponent<FuelPickUp>();
            loot.DropLoot(enemyPosition);
        }
    }

    //public int GetModifiedWorthMoney()
    //{
    //    return Mathf.RoundToInt(worthMoney * worthMoneyMultiplier);
    //}

    private bool CanLootbeDroped()
    {
        //float modifiedChance = dropChance * dropChanceMultiplier;
        Debug.Log("Chance right now: " + dropChance);

        float roll = Random.Range(0f, 100f);
        Debug.Log("Roll was " + roll);

        if (roll < dropChance)
        {
            Debug.Log("Loot was dropped");
            return true;
        }

        return false;
    }
  
}