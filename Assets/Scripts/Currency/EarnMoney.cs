using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarnMoney : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EnemyHealthController enemy;
    //private BulletCollision collidedEnemy;
    public int moneyCount;
    public TextMeshProUGUI coinText;

    void Start()
    {
        //enemy = GetComponent<EnemyHealthController>();
        //collidedEnemy = GetComponent<BulletCollision>();
        moneyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EarnCurrency();
    }

    private void EarnCurrency()
    {
        if(enemy.CheckIfEntityIsAlive()) //TO DO: fix this after the observer pattern is added for managing destruction of the player and the enemy
        {
            moneyCount += enemy.worthMoney;
            UpdateText();
        }
    }



    private void UpdateText()
    {
        coinText.text = " : " + moneyCount.ToString();
    }

}
