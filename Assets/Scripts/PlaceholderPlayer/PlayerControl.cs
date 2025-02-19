using UnityEngine;

// Since we're making the player in a different branch this script might not be needed

//HANDLES THE PLAYER'S ACTIONS IN OTHER WORDS - SHIELDING AND ATTACKING
public class PlayerControl : MonoBehaviour
{
    //public float moveSpeed = 1.0f;

    //private Vector2 moveDirection;
    //private Vector2 mousePosition;

    public Rigidbody2D rb;
    public PlayerAttack weapon;
    //public GameObject target;

    void Update()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");

        if(this == null || weapon == null) { return; }

        if (Input.GetMouseButtonDown(0)) // left click\
        {
            weapon.Fire();
        }
        /*
        else if (Input.GetMouseButtonDown(1)) // right click
        {
            // TODO: Add switching between shield and weapon here
        }
        */


        //moveDirection = new Vector2(moveX, moveY).normalized;
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void FixedUpdate()
    {
        //rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);  

        ////rotate the player with the mouse
        //Vector3 aimDirection = mousePosition - rb.position;
        //float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = aimAngle;
    }
}