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
        }
    }

    public void AddMoney(int amount)
    {
        moneyCount += amount;
        UpdateText();
    }

    public void UpdateAfterPurchase(int amount)
    {
        moneyCount = amount;
        UpdateText();
    }

    private void UpdateText()
    {
        coinText.text = " : " + moneyCount.ToString();
    }

}
