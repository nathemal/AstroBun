using UnityEngine;

public class TutorialEnemySpawn : MonoBehaviour
{
    // Reference to the tutorial enemy prefab (with the TutorialEnemyHealthController attached)
    public GameObject tutorialEnemyPrefab;
    // Reference to the player (or any other logic to determine the spawn position)
    public GameObject Player;

    // Scaling factor for the enemy
    public float scaleFactor = 2f; // Default scaling factor to double the size

    void Start()
    {
        // Spawn the tutorial enemy at a position relative to the player
        SpawnTutorialEnemy(Player.transform.position);
    }

    // Method to spawn the tutorial enemy at a position relative to the player
    void SpawnTutorialEnemy(Vector3 playerPosition)
    {
        // For example, spawn the enemy 20 units in front of the player
        Vector3 spawnPosition = playerPosition + (Vector3.up * 30f);

        // Instantiate the tutorial enemy prefab at the spawn position
        GameObject enemy = Instantiate(tutorialEnemyPrefab, spawnPosition, Quaternion.identity);

        // Scale up the enemy
        ScaleEnemy(enemy);
    }

    // Method to scale the enemy
    void ScaleEnemy(GameObject enemy)
    {
        // Check if the enemy exists
        if (enemy != null)
        {
            // Scale up the enemy by the specified factor
            enemy.transform.localScale *= scaleFactor; // This multiplies the current scale by the factor
            Debug.Log("Enemy scaled up to: " + enemy.transform.localScale);
        }
        else
        {
            Debug.LogError("Enemy object is null!");
        }
    }
}
