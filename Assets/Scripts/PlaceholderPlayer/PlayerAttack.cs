using UnityEngine;

//Handles the player attacks
public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private BulletSettings bulletScript;
    private float firePointRadiusForVisualization = 0.08f;
    private float nextFireTime;

    private void Start()
    {
        bulletScript = projectilePrefab.GetComponent<BulletSettings>();
    }

    public void CreateBullets()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        BulletSettings bulletInfo = bullet.GetComponent<BulletSettings>();

        if (bulletInfo != null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToTarget = (mouseWorldPosition - firePoint.position).normalized;

            bulletInfo.SetDirection(directionToTarget);
        }
    }


    public void Fire()
    {
        //fire according the fire rate
        if (nextFireTime < Time.time)
        {
            CreateBullets();
            nextFireTime = Time.time + bulletScript.fireRate;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(firePoint.transform.position, firePointRadiusForVisualization);
    }
}