using UnityEngine;

public class AllEnemyDataManager : MonoBehaviour
{
    public static AllEnemyDataManager Instance;

    [Header("JUST PUT THIS MANAGER IN ONE LEVEL ONCE")]
    [SerializeField] private EnemyData[] AllEnemyData;
    [SerializeField] private EnemyHealthController[] AllEnemiesList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetAllEnemyData();
        }
    }


    private void ResetAllEnemyData()
    {
        foreach (EnemyData data in AllEnemyData)
        {
            if (data.isNewGame)
            {
                EnemyHealthController enemy = FindEnemyByType(data.enemyType);
                //if (!data.hasStoredDefaults)
                //{
                //    data.SetDefaultStats(enemy);
                //}
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
}
