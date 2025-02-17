using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //This script handles the attacks of the enemy
    public GameObject projectilePrefab;
    public Transform firePoint;
    private BulletSettings bulletScript;
    private float firePointRadiusForVisualization = 0.08f;
    private float nextFireTime;
    public GameObject target;
    private void Start()
    {
        bulletScript = projectilePrefab.GetComponent<BulletSettings>();
    }

    public void CreateBullets(GameObject target)
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        //Set direction where to go for the bullets
        BulletSettings bulletInfo = bullet.GetComponent<BulletSettings>();
        if (bulletInfo != null)
        {
            bulletInfo.SetTarget(target);
        }


    }
    
    public void Fire(GameObject target)
    {
        //fire according the fire rate
        if(nextFireTime < Time.time)
        {
            CreateBullets(target);
            nextFireTime = Time.time + bulletScript.fireRate;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(firePoint.transform.position, firePointRadiusForVisualization);
    }
   
}
