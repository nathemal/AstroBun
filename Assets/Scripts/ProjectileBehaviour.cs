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
        //Color32 bulletColor = gameObject.GetComponent<SpriteRenderer>().color;
        //Color32 enemyColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

        //if (!((collision.gameObject.tag == "Nebula") || (collision.gameObject.tag == "Player")) && 
        //    !((bulletColor.r == enemyColor.r && bulletColor.g == enemyColor.g && bulletColor.b == enemyColor.b)))
        //{
        //    //Debug.Log("enemy color is: " + enemyColor);
        //    Destroy(collision.gameObject);
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}


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
