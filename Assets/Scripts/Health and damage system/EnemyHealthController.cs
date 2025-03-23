using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyHealthController : MonoBehaviour
{
    //public EnemyTypeChoices enemyType;
   
    public float maxHealth;
    public float currentHealth;
    private ChangeEnemyColor enemyColor;

    public int worthMoney;
    private ActivateEnemyDeath enemySoul;

    [Header("To destroy enemy")]
    public UnityEvent<int> onDeath;

    [Header("For Fuel Loot")]
    public GameObject fuelLootPrefab;
    public float dropChance;
    public static float dropChanceMultiplier = 1.0f; //no effect

    [Header("For Heal Loot")]
    public GameObject healLootPrefab;

    [Header("To save data")]
    public EnemyData data; 

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        //if (data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == ""))
        //{
        //    data.SetStatsNextLevel(this);
        //}

        currentHealth = maxHealth;
        enemyColor = GetComponent<ChangeEnemyColor>();
        enemySoul = GetComponent<ActivateEnemyDeath>();

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

            enemySoul.SetSoulActive();

            onDeath.Invoke(worthMoney);
           
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

        //Drop heal loot
        GameObject healLootInstance = Instantiate(healLootPrefab, enemyPosition, Quaternion.identity);

        HealPickUp heal = healLootInstance.GetComponent<HealPickUp>();
        heal.DropLoot(enemyPosition);


        //drop fuel loot
        if (CanLootbeDroped())
        {
            GameObject fuelLootInstance = Instantiate(fuelLootPrefab, enemyPosition, Quaternion.identity);

            FuelPickUp loot = fuelLootInstance.GetComponent<FuelPickUp>();
            loot.DropLoot(enemyPosition);
        }
    }

    private bool CanLootbeDroped()
    {
        //Debug.Log("Chance right now: " + dropChance);

        float roll = Random.Range(0f, 100f);
        //Debug.Log("Roll was " + roll);

        if (roll < dropChance)
        {
            //Debug.Log("Loot was dropped");
            return true;
        }

        return false;
    }
  
}