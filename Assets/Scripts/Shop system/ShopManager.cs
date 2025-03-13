using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PowerUpSetting
{
    public string powerUpName; 
    public string description;
    public float upgradeStat; //how much the current stat will be improved
    public int cost; 
    //public Sprite image;
    [HideInInspector] public GameObject itemRef;
}



public class ShopManager : MonoBehaviour
{
    public BulletSettings currentWeapon; 

    public static ShopManager instance;
    
    public PowerUpSetting[] PoweUpList;

    public EarnMoney totalCoinCount; //how much the player has
    public GameObject shopUI; // toggle on and off the shop UI

    public Transform shopContent;
    public GameObject shopItemPefab;

    private GameObject shopItemParent;

    public EnemyHealthController enemy;
    public Fuelbar fuelTank;
    public PlayerController playerFuel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        foreach(PowerUpSetting powerUpObject in PoweUpList)
        {
            shopItemParent = Instantiate(shopItemPefab, shopContent);

            powerUpObject.itemRef = shopItemParent;


            foreach(Transform child in shopItemParent.transform)
            {
                if(child.gameObject.name == "Item title")
                {
                    CheckOtherChildTitle(child, powerUpObject);
                }
                else if(child.gameObject.name == "descriptions")
                {
                    CheckOtherChildDescription(child, powerUpObject);
                }
                //else if(child.gameObject.name == "Object picture") //if it is needed for later
                //{
                //    child.gameObject.GetComponent<Image>().sprite = powerUpObject.image;
                //}
            }

            Button button = shopItemParent.transform.Find("Buy button")?.GetComponent<Button>();
            if (button != null)
            {
                PowerUpSetting currentPowerUp = powerUpObject;
                button.onClick.AddListener(() => BuyPowerUp(currentPowerUp));
               
            }
        }
    }


    private void CheckOtherChildTitle(Transform child, PowerUpSetting powerUp)
    {
        foreach(Transform grandChildren in child)
        {
            if(grandChildren.gameObject.name == "title text")
            {
                grandChildren.gameObject.GetComponent<TMP_Text>().text = powerUp.powerUpName.ToString();
            }
        }
    }

    private void CheckOtherChildDescription(Transform child, PowerUpSetting powerUp)
    {
        foreach (Transform grandChildren in child)
        {
            if (grandChildren.gameObject.name == "item description")
            {
                grandChildren.gameObject.GetComponent<TMP_Text>().text = powerUp.description.ToString();/// + " " + powerUp.upgradeStat.ToString();
            }
            else if (grandChildren.gameObject.name == "cost description")
            {
                grandChildren.gameObject.GetComponent<TMP_Text>().text = "Cost " + powerUp.cost.ToString();
            }
        }
    }


    public bool CheckIfPlayerHasEnoughMoney(PowerUpSetting powerUp)
    {
        if(totalCoinCount.moneyCount >= powerUp.cost)
        {
            return true;
        }
        return false;
    }

    private void BuyPowerUp(PowerUpSetting powerUp)
    {
        if(CheckIfPlayerHasEnoughMoney(powerUp))
        {
           totalCoinCount.moneyCount -= powerUp.cost;
           Debug.Log($"ShopManager: New money count after purchase: {totalCoinCount.moneyCount}");
           totalCoinCount.OnBuy.Invoke(totalCoinCount.moneyCount);

           ApplyPowerUp(powerUp);

        }
        else
        {
            Debug.Log("Player doesn't have enough money");
        }
    }

    private void ApplyPowerUp(PowerUpSetting powerUp)
    {
        switch(powerUp.powerUpName)
        {
            //powerups for weapon
            case "Fire Rate": //write name that you wrote in shop manager shop item list
                currentWeapon.fireRate = CalculateFireRateUpdate(powerUp.upgradeStat);
                break;
            case "Fire Damage":
                currentWeapon.damage = CalculateProcOfIncreaseStat(currentWeapon.damage, powerUp.upgradeStat);
                break;
            case "Speed": //speed of bullet
                currentWeapon.speed = CalculateProcOfIncreaseStat(currentWeapon.speed, powerUp.upgradeStat);
                break;
            case "Range":
                currentWeapon.lifeSpan = CalculateProcOfIncreaseStat(currentWeapon.lifeSpan, powerUp.upgradeStat);
                break;
            case "Fuel tank":
                 IncreaseFuelTank(powerUp.upgradeStat); //increase capacity of fuel tank
                break;
            case "Fuel consumption":
                playerFuel.fuelConsumptionRate = DecreaseFuelConsumption(powerUp.upgradeStat); //decrease fuel consumption rate
                break;
            case "Dash fuel consumption":
                //code in here
                break;
            case "Currency drop":
                EnemyHealthController.worthMoneyMultiplier *= (1 + (powerUp.upgradeStat / 100.0f));
                Debug.Log("New global currency multiplier: " + EnemyHealthController.worthMoneyMultiplier);
                break;
            case "Fuel loot drop":
                EnemyHealthController.dropChanceMultiplier = IncreaseFuelLootChance(powerUp.upgradeStat);
                Debug.Log("New global drop chance multiplier: " + EnemyHealthController.dropChanceMultiplier);
                break;
        }
    }


    private float CalculateFireRateUpdate(float percentageIncrease)
    {
        return currentWeapon.fireRate * (1 - (percentageIncrease / 100.0f));
    }

    private float CalculateProcOfIncreaseStat(float currentWeaponStat,float percentageIncrease)
    {
        return currentWeaponStat * (1 + (percentageIncrease / 100.0f));
    }

    private float DecreaseFuelConsumption(float percentageIncrease)
    {
        if(playerFuel.fuelConsumptionRate > 0)
        {
            return playerFuel.fuelConsumptionRate * (1 - (percentageIncrease / 100.0f));
        }
        Debug.Log("Fuel consumption cannot be negative");
        return 0;
    }

    private void IncreaseFuelTank(float percentageIncrease)
    {
        fuelTank.fuelBar.maxValue *= (1 + (percentageIncrease / 100.0f));
        
        fuelTank.UpdateFuelText((int)fuelTank.fuelBar.maxValue, playerFuel.fuel);
    }

    private float IncreaseFuelLootChance(float procentage)
    {
        if(procentage > 0.0f && procentage < 100.0f)
        {
            return EnemyHealthController.dropChanceMultiplier * (1 + (procentage / 100.0f)); //return upgraded
        }
        return EnemyHealthController.dropChanceMultiplier; //return not upgraded
    }


    public void OpenShop()
    {
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

}
