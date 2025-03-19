using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
	public EnemyTypeChoices enemyType;

    [SerializeField] private float fuelDropChance;
    [SerializeField] private int worthMoney;
    [SerializeField] public string lastSceneName = "";
    [HideInInspector] public bool isNewGame = true;

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

    private void OnEnable()
    {
        isNewGame = true;
    }
}
