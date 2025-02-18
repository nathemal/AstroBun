using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Transform firePoint;
    //public float fireForce;

    public void Fire()
    {
        GameObject bullet = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
    }
}