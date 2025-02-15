using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject enemy;
    //Detroy bullet and or object that collided with bullet

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.tag == "Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
