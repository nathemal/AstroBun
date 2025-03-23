using UnityEngine;

public class TutorialEnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    public Transform player;       // Assign the player GameObject in the Inspector
    public float spawnDistance = 5f;

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null)
        {
            Debug.LogWarning("EnemyPrefab or Player is missing!");
            return;
        }

        // Generate a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Calculate spawn position
        Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, randomDirection.y, 0) * spawnDistance;

        // Instantiate enemy at spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
