using UnityEngine;
using UnityEngine.Events;


public class DisablePlayerBehaviourInShop : MonoBehaviour
{
    public UnityEvent nearShop;
    public UnityEvent farShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            nearShop.Invoke();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            farShop.Invoke();
        }
    }
    
}
