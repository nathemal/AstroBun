using UnityEngine;

public class AllEntityDataManager : MonoBehaviour
{
    public static AllEntityDataManager Instance;

    [Header("JUST PUT THIS MANAGER IN ONE LEVEL ONCE")]
    [SerializeField] private PlayerData playerData; 

    private PlayerHealthController playerHealth;
    private PlayerController playerFuel;
    private PlayerAttack playerAttack;
    [SerializeField] public EnemyData[] AllEnemyData;
    [SerializeField] public EnemyHealthController[] AllEnemiesList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
 
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        FindPlayerComponents();
        ResetAllEnemyData();
        ResetPlayerData();
    }

    private void FindPlayerComponents()
    {
        playerHealth = Object.FindFirstObjectByType<PlayerHealthController>();
        playerFuel = Object.FindFirstObjectByType<PlayerController>();
        playerAttack = Object.FindFirstObjectByType<PlayerAttack>();
    }

    public void ResetPlayerData()
    {
        playerData.UpdateHealthInFirstScene(playerHealth);

        playerData.UpdateFuelDataInFirstScene(playerFuel);

        playerData.SetBulletDefaultStats(playerAttack.bulletScript);
        playerData.ResetBulletDefaultStats();
    }

    public void ResetAllEnemyData()
    {
        foreach (EnemyData data in AllEnemyData)
        {
            EnemyHealthController enemy = FindEnemyByType(data.enemyType);

            data.SetDefaultStats(enemy);

            data.ResetStats();
        }
    }

    private EnemyHealthController FindEnemyByType(EnemyTypeChoices type)
    {
        
        foreach(EnemyHealthController enemy in AllEnemiesList)
        {
            if(enemy.data.enemyType == type)
            {
                return enemy;
            }
        }
        return null;
    }


    public void UpdatePlayerStats()
    {
        playerData.UpdateHealthInNextScene(playerHealth);
        playerData.UpdateFuelDataInNextScene(playerFuel);
    }

    public void UpdateEnemyStats()
    {
        foreach (EnemyData data in AllEnemyData)
        {
            EnemyHealthController enemy = FindEnemyByType(data.enemyType);

            data.SetStatsNextLevel(enemy);

        }
    }
}
