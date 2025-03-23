using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;


public class DisableEntityBehaviourInShop : MonoBehaviour
{
    public UnityEvent nearShop;
    public UnityEvent farShop;
    private HashSet<GameObject> EnemiesInsideShopArea = new HashSet<GameObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nearShop.Invoke();
        }
        else if(collision.tag == "Enemy")
        {
            if (!EnemiesInsideShopArea.Contains(collision.gameObject))
            {
                EnemiesInsideShopArea.Add(collision.gameObject);
            }
            HandleAllEnemiesShooting(false, collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            farShop.Invoke();
        }
        else if (collision.tag == "Enemy")
        {
            if (EnemiesInsideShopArea.Contains(collision.gameObject))
            {
                EnemiesInsideShopArea.Remove(collision.gameObject);
            }
            HandleAllEnemiesShooting(true, collision.gameObject);
        }
    }

    private void HandleAllEnemiesShooting(bool canShoot, GameObject enemy)
    {
        EnemyAttack enemyAttack = enemy.GetComponent<EnemyAttack>();

        if (enemyAttack == null) { return; }

        if (canShoot)
        {
            enemyAttack.EnableShooting();
        }
        else
        {
            enemyAttack.DisableShooting();
        }
    }
}
