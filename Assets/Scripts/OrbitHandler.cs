using UnityEngine;

/*
    This script should handle the entry and exit of orbits
*/

public class OrbitHandler : MonoBehaviour
{
    Rigidbody2D rb;

    //static float orbitRadius = 1.5f;
    //static float orbitSpeed = 3f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() // To avoid tying the game physics to fps FixedUpdate is used instead of Update here
    {
        
    }
}
