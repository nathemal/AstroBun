using UnityEngine;
using UnityEngine.SceneManagement;

//Handles the player attacks
public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public BulletSettings bulletScript;
    private float firePointRadiusForVisualization = 0.08f;
    private float nextFireTime;
    [Header("Saving Player Data")]
    public PlayerData data;

    private void Start()
    {
        bulletScript = projectilePrefab.GetComponent<BulletSettings>();

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (bulletScript != null && data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == "")) //it is kinda works?
        {
            bulletScript.fireRate = data.FireRateValue;
            bulletScript.damage = data.FireDamageValue;
            bulletScript.speed = data.BulletSpeedValue;
            bulletScript.lifeSpan = data.ShootingRangeValue;
        }
        else
        {
            data.FireRateValue = bulletScript.fireRate;
            data.FireDamageValue = bulletScript.damage;
            data.BulletSpeedValue = bulletScript.speed;
            data.ShootingRangeValue = bulletScript.lifeSpan;
        }

           
    }

    public void CreateBullets()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        BulletSettings bulletInfo = bullet.GetComponent<BulletSettings>();

        if (bulletInfo != null)
        {
            //string currentSceneName = SceneManager.GetActiveScene().name;
            //if (bulletScript != null && data != null && data.lastSceneName != currentSceneName && !(data.lastSceneName == "")) //it is kinda works?
            //{
            //    bulletScript.fireRate = data.FireRateValue;
            //    bulletScript.damage = data.FireDamageValue;
            //    bulletScript.speed = data.BulletSpeedValue;
            //    bulletScript.lifeSpan = data.ShootingRangeValue;

            //}

            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Vector2 directionToTarget = mouseWorldPosition - firePoint.position;

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