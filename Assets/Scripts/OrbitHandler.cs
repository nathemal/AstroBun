using UnityEngine;

public class OrbitHandler : MonoBehaviour
{
    Rigidbody2D rb;
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
