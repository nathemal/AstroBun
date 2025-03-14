using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float fuelDropChance;
    [SerializeField] private int worthMoney;
    [SerializeField] public string lastSceneName;
    public float FuelDropChanceValue
	{
		get { return fuelDropChance; }
		set { fuelDropChance = value; }
	}

	public int WorthMoneyValue
	{
		get { return worthMoney; }
		set { worthMoney = value; }
	}
}
