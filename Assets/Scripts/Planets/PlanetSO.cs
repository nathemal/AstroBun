using UnityEngine;

public abstract class PlanetSO: ScriptableObject
{
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => _sprite;

    public string planetName;
    public float size = 3f;
    public Color color;
    public float gravityStrength = 0.2f;
}