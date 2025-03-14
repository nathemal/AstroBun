using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("The player's data")]
    [SerializeField] private float health;
    [SerializeField] private float fuelAmount;
    [SerializeField] private float fuelTankCapacity;
    [SerializeField] private float fuelUsageAmount;

    [Header("The player's weapon data after powerups purchase")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeSpan; //range
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;

    [Header("The player's weapon original data")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defualtLifeSpan; //range
    [SerializeField] private float defaultDamage;
    [SerializeField] private float defualtFireRate;

    [SerializeField] public string lastSceneName = "";
    [HideInInspector] public bool isNewGame = true;
    [HideInInspector] public bool hasStoredDefaults = false;
    public float HealthValue
	{
		get { return health; }
		set { health = value; }
	}

    public float FuelAmountValue
    {
        get { return fuelAmount; }
        set { fuelAmount = value; }
    }

    public float FuelTankCapValue
    {
        get { return fuelTankCapacity; }
        set { fuelTankCapacity = value; }
    }

    public float FueConsumptionValue
    {
        get { return fuelUsageAmount; }
        set { fuelUsageAmount = value; }
    }

    public float BulletSpeedValue
    {
        get { return speed; }
        set { speed = value; }
    }

    public float ShootingRangeValue
    {
        get { return lifeSpan; }
        set { lifeSpan = value; }
    }
    public float FireDamageValue
    {
        get { return damage; }
        set { damage = value; }
    }
    public float FireRateValue
    {
        get { return fireRate; }
        set { fireRate = value; }
    }


    public float BulletSpeedDefaultValue
    {
        get { return defaultSpeed; }
        set { defaultSpeed = value; }
    }

    public float ShootingRangeDefaultValue
    {
        get { return defualtLifeSpan; }
        set { defualtLifeSpan = value; }
    }
    public float FireDamageDefaultValue
    {
        get { return defaultDamage; }
        set { defaultDamage = value; }
    }
    public float FireRateDefaultValue
    {
        get { return defualtFireRate; }
        set { defualtFireRate = value; }
    }

    private void OnEnable()
    {
        isNewGame = true;
    }
}
