using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform firePoint;
    public float fireForce;

    public void Fire()
    {
        GameObject bullet = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
        //ProjectileBehaviour bullet = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}
