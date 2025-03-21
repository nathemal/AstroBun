using UnityEngine;

public class AllEntityDataManager : MonoBehaviour
{
    public static AllEntityDataManager Instance;

    [Header("JUST PUT THIS MANAGER IN ONE LEVEL ONCE")]
    [SerializeField] private PlayerData playerData;

    [SerializeField] public PlayerHealthController playerHealth;
    [SerializeField] public PlayerController playerFuel;
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] public EnemyData[] AllEnemyData;
    [SerializeField] public EnemyHealthController[] AllEnemiesList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            //playerHealth = FindObjectOfType<PlayerHealthController>();

            DontDestroyOnLoad(gameObject);
            ResetAllEnemyData(); //letter to comment out
            ResetPlayerData();
        }
    }

    public void ResetPlayerData()
    {
        //if (playerData.isNewGame)
        //{
        //    playerData.UpdateHealthInFirstScene(playerHealth);

        //    playerData.UpdateFuelDataInFirstScene(playerFuel);

        //    playerData.SetBulletDefaultStats(playerAttack.bulletScript);
        //    playerData.ResetBulletDefaultStats();

        //    playerData.isNewGame = false;
        //}

        playerData.UpdateHealthInFirstScene(playerHealth);

        playerData.UpdateFuelDataInFirstScene(playerFuel);

        playerData.SetBulletDefaultStats(playerAttack.bulletScript);
        playerData.ResetBulletDefaultStats();
    }

    public void ResetAllEnemyData()
    {
        foreach (EnemyData data in AllEnemyData)
        {
            if (data.isNewGame)
            {
                EnemyHealthController enemy = FindEnemyByType(data.enemyType);
                
                data.SetDefaultStats(enemy);

                data.ResetStats();
                data.isNewGame = false;
            }
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
