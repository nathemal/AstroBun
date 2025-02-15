using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Graviton : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 startPosition;
    Vector2 movementInput;
    public float rotationSpeed = 200f;

    public bool IsAttractee
    {
        get
        {
            return isAttractee;
        }
        set
        {
            if (value == true)
            {
                if (!GravityHandler.attractees.Contains(this.GetComponent<Rigidbody2D>()))
                    GravityHandler.attractees.Add(rb);
            }
            else if (value == false)
                GravityHandler.attractees.Remove(rb);

            isAttractee = value;
        }
    }
    public bool IsAttractor
    {
        get
        {
            return isAttractor;
        }
        set
        {
            if (value == true)
            {
                if (!GravityHandler.attractors.Contains(this.GetComponent<Rigidbody2D>()))
                    GravityHandler.attractors.Add(rb);
            } 
            else if (value == false)
                GravityHandler.attractors.Remove(rb);

            isAttractor = value;
            startPosition = GetComponent<Transform>().position;
        }
    }

    [SerializeField] bool isAttractee;
    [SerializeField] bool isAttractor;
    
    [SerializeField] Vector3 initialVelocity;
    [SerializeField] bool applyInitialVelocityOnStart;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        IsAttractee = isAttractee;
        IsAttractor = isAttractor;
    }

    void Start()
    {
        if (applyInitialVelocityOnStart)
            rb.AddForce(initialVelocity, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (isAttractor)
            GetComponent<Transform>().position = startPosition; // TODO: This might not be needed anymore
    }
}