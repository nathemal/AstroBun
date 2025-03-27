using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoadLevel : MonoBehaviour
{
    public string nextLevelName;
    [SerializeField] public List<EnemyData> EnemiesDatasList;
    [SerializeField] public PlayerData playerData;
    public PlayerHealthController playerHealth;

    public string currentSceneName;
    private List<string> levelSequence = new List<string> { "LevelOne", "LevelTwo", "LevelThree" };

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        LoadSceneWhenAllEnemiesAreDead();
    }

    private void LoadSceneWhenAllEnemiesAreDead()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("Enemy count: " + enemyCount);

        playerData.lastSceneName = currentSceneName;
        foreach (EnemyData enemyData in EnemiesDatasList)
        {
            enemyData.lastSceneName = currentSceneName;
        }

        if (playerHealth.currentHealth > 0 && enemyCount <= 0)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        nextLevelName = GetNextLevel();

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
            UpdateGameData();
        }
        else
        {
            Debug.Log("Player Wins!");
            ResetGameData();
            SceneManager.LoadScene("WinScene");
        }
    }

    private string GetNextLevel()
    {
        int currentIndex = levelSequence.IndexOf(currentSceneName);
        if (currentIndex == -1)
        {
            Debug.LogError("Current scene is not in the level sequence!");
            return null;
        }

        int nextIndex = (currentIndex + 1) % levelSequence.Count;
        return levelSequence[nextIndex];
    }

    private void UpdateGameData()
    {
        if (AllEntityDataManager.Instance != null)
        {
            AllEntityDataManager.Instance.UpdateEnemyStats();
            AllEntityDataManager.Instance.UpdatePlayerStats();
        }
    }

    private void ResetGameData()
    {
        if (AllEntityDataManager.Instance != null)
        {
            AllEntityDataManager.Instance.ResetPlayerData();
            AllEntityDataManager.Instance.ResetAllEnemyData();
        }
    }




}
