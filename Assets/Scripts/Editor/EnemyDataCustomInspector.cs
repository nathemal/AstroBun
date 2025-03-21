using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Sirenix.OdinValidator.Editor;

//[CustomEditor(typeof(EnemyData))]
public class EnemyDataCustomInspector : Editor
{
    public VisualTreeAsset VisualTree;
    //private EnemyData enemyData;

    private void OnEnable()
    {
        //enemyData = (EnemyData)target;

        //if (enemyData == null)
        //{
        //    Debug.LogError("EnemyData is NULL!");
        //}
        //else
        //{
        //    Debug.Log($"EnemyData Loaded: {enemyData.name}");
        //}


        //serializedObject.Update();

    }
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        VisualTree.CloneTree(root);

        serializedObject.Update();

        InitPanel(root.Q<VisualElement>("EasyEnemyPanel"), serializedObject, EnemyTypeChoices.Easy);
        InitPanel(root.Q<VisualElement>("NormalEnemyPanel"), serializedObject, EnemyTypeChoices.Normal);
        InitPanel(root.Q<VisualElement>("StrongEnemyPanel"), serializedObject, EnemyTypeChoices.Strong);
        InitPanel(root.Q<VisualElement>("StrongestEnemyPanel"), serializedObject, EnemyTypeChoices.Strongest);

        serializedObject.ApplyModifiedProperties();

        return root;
    }

  
    private void InitPanel(VisualElement panel, SerializedObject serializedObject, EnemyTypeChoices type)
    {

        SerializedProperty defaultFuelDropChance = serializedObject.FindProperty("defaultFuelDropChance");
        SerializedProperty defaultWorthMoney = serializedObject.FindProperty("defaultWorthMoney");
       

        SerializedProperty fuelDropChance = serializedObject.FindProperty("fuelDropChance");
        SerializedProperty worthMoney= serializedObject.FindProperty("worthMoney");

        Debug.Log($"fuelDropChance: {fuelDropChance.floatValue}, worthMoney: {worthMoney.intValue}");
       
        PropertyField defaultFuelDropChanceField = panel.Query<PropertyField>("DefaultFuelDropChanceField").First();
        PropertyField defaultWorthMoneyField = panel.Query<PropertyField>("DefaultWorthMoneyField").First();

        PropertyField fuelDropChanceField = panel.Query<PropertyField>("FuelDropChanceField").First();
        PropertyField worthMoneyField = panel.Query<PropertyField>("WorthMoneyField").First();


        Debug.Log(defaultFuelDropChanceField == null ? " DefaultFuelDropChanceField NOT found" : "DefaultFuelDropChanceField found");
        Debug.Log(defaultWorthMoneyField == null ? " DefaultWorthMoneyField NOT found" : "DefaultWorthMoneyField found");
        Debug.Log(fuelDropChanceField == null ? "FuelDropChanceField NOT found" : "FuelDropChanceField found");
        Debug.Log(worthMoneyField == null ? "WorthMoneyField NOT found" : "WorthMoneyField found");


        if (defaultFuelDropChanceField != null)
        {
            defaultFuelDropChanceField.BindProperty(defaultFuelDropChance);
            Debug.Log("Bound DefaultFuelDropChanceField");
        }


        if (defaultWorthMoneyField != null)
        {
            defaultWorthMoneyField.BindProperty(defaultWorthMoney);
            Debug.Log("Bound DefaultWorthMoneyField");
        }


        if (fuelDropChanceField != null)
        {
            fuelDropChanceField.BindProperty(fuelDropChance);
            Debug.Log("Bound FuelDropChanceField");
        }

        if (worthMoneyField != null)
        {
            worthMoneyField.BindProperty(worthMoney);
            Debug.Log("Bound WorthMoneyField");
        }

    }
}
