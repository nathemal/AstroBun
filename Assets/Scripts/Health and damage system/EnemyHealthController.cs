using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyHealthController : MonoBehaviour
{
    private GameObject audioManager;
    private SoundManager sound;

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

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager");

        sound = audioManager.GetComponent<SoundManager>();
    }

    private void Start()
    {   
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

        sound.enemyTakingDamage.Play();

        if (enemyColor != null)
        {
            enemyColor.ChangeSpriteColor();
        }

        if (currentHealth <= 0)
        {
            DropTheLoot();
           
            sound.enemyDying.Play();

            StartCoroutine(HandleEnemyDeath());
        }
    }

    private IEnumerator HandleEnemyDeath()
    {
        enemySoul.SetSoulActive();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<EnemyChase>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        onDeath.Invoke(data.WorthMoneyValue);
        Destroy(gameObject);
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
        //Debug.Log("Chance right now: " + data.FuelDropChanceValue);

        float roll = Random.Range(0f, 100f);
        //Debug.Log("Roll was " + roll);

        if (roll < data.FuelDropChanceValue)
        {
            return true;
        }

        return false;
    }
}