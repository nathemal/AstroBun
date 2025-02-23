using UnityEngine;

// Since we're making the player in a different branch this script might not be needed

//HANDLES THE PLAYER'S ACTIONS IN OTHER WORDS - SHIELDING AND ATTACKING
public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerAttack weapon;
    //public GameObject target;

    void Update()
    {
        if(this == null || weapon == null) 
            return;

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
    }

    
}