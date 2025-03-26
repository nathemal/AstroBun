using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{

    [SerializeField] public EnemyTypeChoices enemyType;

    [Header("The enemy default data before powerups purchase")]
    [SerializeField] private float defaultFuelDropChance;
    [SerializeField] private int defaultWorthMoney;

    [Header("The enemy data after powerups purchase")]
    [SerializeField] private float fuelDropChance;
    [SerializeField] private int worthMoney;

    [SerializeField] public string lastSceneName = "";
    [HideInInspector] public bool isNewGame = true;
    [HideInInspector] public bool hasStoredDefaults = false;

    public float DefaultFuelDropChanceValue
    {
        get { return defaultFuelDropChance; }
        set { defaultFuelDropChance = value; }
    }

    public int DefaultWorthMoneyValue
    {
        get { return defaultWorthMoney; }
        set { defaultWorthMoney = value; }
    }

    public float FuelDropChanceValue
    {
        get { return fuelDropChance; }
        set { fuelDropChance = value; }
    }

    public int WorthMoneyValue
    {
        get { return worthMoney; }
        set { worthMoney = value; }
    }

    public void SetDefaultStats(EnemyHealthController enemy)
    {
        if (enemy == null || enemy.currentHealth < 0) { return; }

        if (!hasStoredDefaults)
        {
            DefaultFuelDropChanceValue = enemy.dropChance;
            DefaultWorthMoneyValue = enemy.worthMoney;
            hasStoredDefaults = true;
        }
    }

    public void ResetStats()
    {
        FuelDropChanceValue = DefaultFuelDropChanceValue;
        WorthMoneyValue = DefaultWorthMoneyValue;
    }

    public void SetStatsNextLevel(EnemyHealthController enemy)
    {
        if(enemy == null || enemy.currentHealth < 0) { return; }

        enemy.worthMoney = WorthMoneyValue;
        enemy.dropChance = FuelDropChanceValue;
    }

    private void OnEnable()
    {
        isNewGame = true;
        hasStoredDefaults = false;
    }

}
