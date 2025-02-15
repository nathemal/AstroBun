using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //for more complicated stuff
    public GameObject projectilePrefab;
    public Transform firePoint;
    //public float fireForce;
    private EnemyBullet enemyBulletScript;
    private float firePointRadiusForVisualization = 0.08f;
    private float nextFireTime;

    private void Start()
    {
        enemyBulletScript = projectilePrefab.GetComponent<EnemyBullet>();
    }

    public void CreateBullets()
    {
       Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
    
    public void Fire()
    {
        //fire according the fire rate
        if(nextFireTime < Time.time)
        {
            CreateBullets();
            nextFireTime = Time.time + enemyBulletScript.fireRate;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(firePoint.transform.position, firePointRadiusForVisualization);
    }
   
}
