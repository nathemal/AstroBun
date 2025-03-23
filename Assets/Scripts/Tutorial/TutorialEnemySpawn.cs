using UnityEngine;

public class TutorialEnemySpawn : MonoBehaviour
{

    // Reference to the tutorial enemy prefab (with the TutorialEnemyHealthController attached)
    public GameObject tutorialEnemyPrefab;
    // Reference to the player (or any other logic to determine the spawn position)
    public GameObject Player;

    void Start()
    {
        // Spawn the tutorial enemy at a position relative to the player
        SpawnTutorialEnemy(Player.transform.position);
    }

    // Method to spawn the tutorial enemy at a position relative to the player
    void SpawnTutorialEnemy(Vector3 playerPosition)
    {
        // For example, spawn the enemy 20 units in front of the player
        Vector3 spawnPosition = playerPosition + (Vector3.up * 20f);

        // Instantiate the tutorial enemy prefab at the spawn position
        Instantiate(tutorialEnemyPrefab, spawnPosition, Quaternion.identity);
    }
}