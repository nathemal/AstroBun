using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;


public class DisableEntityBehaviourInShop : MonoBehaviour
{
    public UnityEvent nearShop;
    public UnityEvent farShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nearShop.Invoke();
        }
        else if(collision.tag == "Enemy")
        {
            HandleAllEnemiesShooting(false);
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
            HandleAllEnemiesShooting(true);
        }
    }

    private void HandleAllEnemiesShooting(bool canShoot)
    {
        GameObject[] EnemiesList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in EnemiesList)
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


    
}
