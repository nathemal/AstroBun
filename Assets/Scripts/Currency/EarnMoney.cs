using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarnMoney : MonoBehaviour
{
    public int moneyCount = 0;
    [Header("Currency HUD is prefab. Add the text of currency HUD from the scene, not from prefab folder")]
    public TextMeshProUGUI coinText;

    void Start()
    {
       
        
        
        //UpdateText();
    }

    public void AddMoney(int amount)
    {
        moneyCount += amount;
        UpdateText();
    }

    private void UpdateText()
    {
        coinText.text = " : " + moneyCount.ToString();
    }

}
