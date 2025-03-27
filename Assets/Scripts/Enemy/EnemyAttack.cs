using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject target;
    public Transform firePoint;

    private BulletSettings bulletScript;
    private float firePointRadiusForVisualization = 0.08f;
    private float nextFireTime;
    private bool canEnemyShoot;

    [Header("Attack Particle settings")]
    public ParticleBurst attackParticles;

    private GameObject audioManager;
    private SoundManager sound;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager");

        sound = audioManager.GetComponent<SoundManager>();
    }

    private void Start()
    {
        bulletScript = projectilePrefab.GetComponent<BulletSettings>();

        canEnemyShoot = true;
    }

    public void CreateBullets(GameObject target)
    {
        if (target == null || this.gameObject == null) { return; }

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        //Set direction where to go for the bullets
        BulletSettings bulletInfo = bullet.GetComponent<BulletSettings>();
        if (bulletInfo != null)
        {
            Vector2 directionToTarget = (target.transform.position - firePoint.position).normalized;
            bulletInfo.SetDirection(directionToTarget);

            attackParticles.Burst(bulletInfo.attackParticlesAmount);
        }
    }
    
    public void Fire(GameObject target)
    {
        if (target == null || this.gameObject == null) { return; }

        //fire according the fire rate
        if (nextFireTime < Time.time && this.gameObject != null && canEnemyShoot)
        {
            CreateBullets(target);
            sound.enemyShooting.Play();
            nextFireTime = Time.time + bulletScript.fireRate;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(firePoint.transform.position, firePointRadiusForVisualization);
    }


    public void DisableShooting()
    {
        canEnemyShoot = false;
    }

    public void EnableShooting()
    {
        canEnemyShoot = true;
    }
}