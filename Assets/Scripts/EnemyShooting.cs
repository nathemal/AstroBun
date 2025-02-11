using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //for more complicated stuff
    public GameObject ProjectilePrefab;
    public Transform firePoint;
    public float fireForce;
    private float firePointRadiusForVisualization = 0.1f;

    public void Fire()
    {
        GameObject bullet = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
        //ProjectileBehaviour bullet = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(firePoint.transform.position, firePointRadiusForVisualization);
    }
}
