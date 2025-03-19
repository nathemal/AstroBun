using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataCustomInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        return base.CreateInspectorGUI();
    }
}
