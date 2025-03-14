using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadLevel : MonoBehaviour
{
    public string levelName;
    [SerializeField] public EnemyData enemyData;
    [SerializeField] public PlayerData playerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        playerData.lastSceneName = SceneManager.GetActiveScene().name;
        enemyData.lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName);
    }
}
