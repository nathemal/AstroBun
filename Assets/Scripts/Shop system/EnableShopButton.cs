using UnityEngine;
using UnityEngine.UI;

public class EnableShopButton : MonoBehaviour
{
    public Button shopButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shopButton != null)
        {
            shopButton.interactable = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EenableShopButton();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DisableShopButton();
        }
    }
   

    private void EenableShopButton()
    {
        if (shopButton != null && !shopButton.interactable)
        {
            shopButton.interactable = true;
        }
    }

    private void DisableShopButton()
    {
        if (shopButton != null && shopButton.interactable)
        {
            shopButton.interactable = false;
        }
    }


   
}
