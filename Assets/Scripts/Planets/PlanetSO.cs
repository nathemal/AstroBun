using UnityEngine;

public abstract class PlanetSO : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites; // Store multiple sprites

    public string planetName;
    public float size = 1f;
    public Color color;
    public float gravityStrength = 0.2f;

    public Sprite GetRandomSprite()
    {

        if (_sprites == null || _sprites.Length == 0) return null;
        return _sprites[Random.Range(0, _sprites.Length)]; 
    }
}