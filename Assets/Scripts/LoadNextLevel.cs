using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class LoadNextLevel : MonoBehaviour
{
    public string nextLevelName;
    public PlayerData playerData;
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

        if (playerHealth.currentHealth > 0 && enemyCount <= 0)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        nextLevelName = GetNextLevel();

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
            yield return new WaitForSeconds(0.5f);

            UpdateGameData();
        }
        else
        {
            ResetGameData();
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("WinScene");
        }

        yield return null;
    }

    private string GetNextLevel()
    {
        int currentIndex = levelSequence.IndexOf(currentSceneName);
        if (currentIndex == -1)
        {
            Debug.LogError("Current scene is not in the level sequence");
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
