using UnityEngine;

public class Planet : MonoBehaviour
{
    public PlanetSO planetData;
    private CircleCollider2D planetCollider;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        planetCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (planetCollider == null)
        {
            planetCollider = gameObject.AddComponent<CircleCollider2D>();
        }
    }

    public void SetPlanetData(PlanetSO data)
    {
        planetData = data;
        gameObject.name = planetData.planetName;
        //Debug.Log($"Planet Set: {planetData.planetName}, Gravity: {planetData.gravityStrength}");

        transform.localScale = Vector3.one * planetData.size;  


        if (spriteRenderer != null)
        {
            spriteRenderer.color = planetData.color; 
            spriteRenderer.sprite = planetData.GetRandomSprite(); 
        }
    }


}
