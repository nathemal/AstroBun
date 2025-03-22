using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("The player's data")]
    [SerializeField] private float health;
    [SerializeField] private float fuelAmount;
    [SerializeField] private float fuelTankCapacity;
    [SerializeField] private float fuelUsageAmount;


    [Header("The player's weapon original data")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defualtLifeSpan; //range
    [SerializeField] private float defaultDamage;
    [SerializeField] private float defualtFireRate;

    [Header("The player's weapon data after powerups purchase")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeSpan; //range
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;

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

    public void SetBulletDefaultStats(BulletSettings bullet)
    {
        if (!hasStoredDefaults)
        {
            FireRateDefaultValue = bullet.fireRate;
            FireDamageDefaultValue = bullet.damage;
            BulletSpeedDefaultValue = bullet.speed;
            ShootingRangeDefaultValue = bullet.lifeSpan;
            hasStoredDefaults = true;
        }
    }
    public void ResetBulletDefaultStats()
    {
        FireRateValue = FireRateDefaultValue;
        FireDamageValue = FireDamageDefaultValue;
        BulletSpeedValue = BulletSpeedDefaultValue;
        ShootingRangeValue = ShootingRangeDefaultValue;
    }

    public void UpdateFuelDataInNextScene(PlayerController player)
    {
        if (player == null) { return; }

        player.fuel = FuelAmountValue;
        player.fuelConsumptionRate = FueConsumptionValue;

        player.fuelTank.UpdateFuelTank(FuelTankCapValue, player.fuel);
    }
    public void UpdateFuelDataInFirstScene(PlayerController player)
    {
        if (player == null) { return; }

        player.fuelTank.UpdateFuelTank(player.fuel, player.fuel);
        FuelTankCapValue = player.fuelTank.fuelBar.maxValue;
        FueConsumptionValue = player.fuelConsumptionRate;
        FuelAmountValue = player.fuel;
    }

    public void UpdateHealthInNextScene(PlayerHealthController player)
    {
        if(player == null || player.currentHealth < 0) { return; }

        player.currentHealth = HealthValue;
        player.Healthbar.UpdateHealthBar(player.maxHealth, player.currentHealth);
    }

    public void UpdateHealthInFirstScene(PlayerHealthController player)
    {
        if (player == null) { return; }

        player.currentHealth = player.maxHealth;
        player.Healthbar.UpdateHealthBar(player.maxHealth, player.currentHealth);
        HealthValue = player.currentHealth;
    }



    private void OnEnable()
    {
        isNewGame = true;
    }

}
