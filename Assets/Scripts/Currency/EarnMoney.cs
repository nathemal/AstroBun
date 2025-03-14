using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EarnMoney : MonoBehaviour
{
    public int moneyCount = 0;
    [Header("Currency HUD is prefab. Add the text of currency HUD from the scene, not from prefab folder")]
    public TextMeshProUGUI coinText;
    public UnityEvent<int> OnBuy;
    //[SerializeField] public PlayerData data;

    void Start()
    {
        ShopManager shopManager = FindAnyObjectByType<ShopManager>();
        if (shopManager != null)
        {
            OnBuy.AddListener(UpdateAfterPurchase);
            //Debug.Log("EarnMoney: OnBuy listener added");
        }
    }

    public void AddMoney(int amount)
    {
        //Debug.Log("Money that needs to be added before addition: " + amount);
        //Debug.Log("Money before addition: " + moneyCount);
        moneyCount += amount;
        UpdateText();
        //Debug.Log("Money after addition: " + moneyCount);
    }

    public void UpdateAfterPurchase(int amount)
    {
        //Debug.Log($"EarnMoney: Updating money from {moneyCount} to {amount}");
        moneyCount = amount;
        UpdateText();
    }

    private void UpdateText()
    {
        coinText.text = " : " + moneyCount.ToString(); //moneyCount
    }

}
