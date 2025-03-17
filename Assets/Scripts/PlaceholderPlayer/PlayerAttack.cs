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

        //string currentSceneName = SceneManager.GetActiveScene().name;

        /*Load data
        if (data.isNewGame)
        {
            SetBulletDefaultStats();
            ResetBulletDefaultStats();

            data.isNewGame = false;
        }
      
        data.lastSceneName = currentSceneName;*/
    }

   /* private void SetBulletDefaultStats()
    {
        if (!data.hasStoredDefaults) 
        {
            data.FireRateDefaultValue = bulletScript.fireRate;
            data.FireDamageDefaultValue = bulletScript.damage;
            data.BulletSpeedDefaultValue = bulletScript.speed;
            data.ShootingRangeDefaultValue = bulletScript.lifeSpan;
            data.hasStoredDefaults = true;
        }
    }

    private void ResetBulletDefaultStats()
    {
        data.FireRateValue = data.FireRateDefaultValue;
        data.FireDamageValue = data.FireDamageDefaultValue;
        data.BulletSpeedValue = data.BulletSpeedDefaultValue;
        data.ShootingRangeValue = data.ShootingRangeDefaultValue;
    }*/

    public void CreateBullets()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        BulletSettings bulletInfo = bullet.GetComponent<BulletSettings>();

        if (bulletInfo != null)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Vector2 directionToTarget = mouseWorldPosition - firePoint.position;

            bulletInfo.SetDirection(directionToTarget);

            /*Upgrade bullet stats after purchase
            bulletInfo.fireRate = data.FireRateValue;
            bulletInfo.damage = data.FireDamageValue;
            bulletInfo.speed = data.BulletSpeedValue;
            bulletInfo.lifeSpan = data.ShootingRangeValue;*/
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