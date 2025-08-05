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

    void Start()
    {
        ShopManager shopManager = FindAnyObjectByType<ShopManager>();
        if (shopManager != null)
        {
            OnBuy.AddListener(SubtractMoney);
        }
    }

    public void AddMoney(int amount)
    {
        moneyCount += amount;
        coinText.text = " : " + moneyCount.ToString();
    }

    public void SubtractMoney(int amount)
    {
        coinText.text = " : " + amount.ToString();
    }
}
