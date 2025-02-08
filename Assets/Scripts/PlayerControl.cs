using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Rigidbody2D rb;
    
    public WeaponBehaviour weapon;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
     
        if (Input.GetMouseButtonDown(0)) //left click
        {
            weapon.Fire();
        }
        //else if (Input.GetMouseButtonDown(1)) //right lick
        //{

        //}

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);  

        //rotate the player with the mouse
        Vector3 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
