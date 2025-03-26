using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class LoadLevel : MonoBehaviour
{
    public string levelName;
    [SerializeField] public List<EnemyData> EnemiesDatasList;
    [SerializeField] public PlayerData playerData;


    public void LoadScene()
    {
        playerData.lastSceneName = SceneManager.GetActiveScene().name;

        foreach(EnemyData enemyData in EnemiesDatasList)
        {
            enemyData.lastSceneName = SceneManager.GetActiveScene().name;
        }
        
        SceneManager.LoadScene(levelName);
    }
}
