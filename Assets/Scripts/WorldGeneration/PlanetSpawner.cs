using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public float radius = 1f;
    public Vector2 regionSize = new Vector2(10, 10);
    public int rejectionSamples = 30;

    public GameObject planetPrefab; // Assign a planet prefab in the inspector
    public PlanetSO[] planetTypes; // Assign different planet data in the inspector

    private List<Vector2> points;
    private List<GameObject> spawnedPlanets = new List<GameObject>();

    void Start()
    {
        GeneratePlanets();
    }

    void GeneratePlanets()
    {
        foreach (GameObject planet in spawnedPlanets)
        {
            Destroy(planet);
        }
        spawnedPlanets.Clear();

        // Generate new positions using Poisson Disk Sampling
        points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);


        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                SpawnPlanet(point);
            }
        }
    }

    void SpawnPlanet(Vector2 position)
    {

        if (planetPrefab == null || planetTypes.Length == 0)
        {
            Debug.LogError("Planet Prefab or Planet Types not assigned!");
            return;
        }

        GameObject planet = Instantiate(planetPrefab, position, Quaternion.identity);
        spawnedPlanets.Add(planet);

        // Assign random planet data
        Planet planetComponent = planet.GetComponent<Planet>();
        if (planetComponent != null)
        {
            planetComponent.SetPlanetData(planetTypes[Random.Range(0, planetTypes.Length)]);
        }
       // Debug.Log($"Spawned {planet.name} at {planet.transform.position} with scale {planet.transform.localScale}");
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(regionSize / 2, regionSize);


        if (points != null)
        {
            Gizmos.color = Color.cyan;
            foreach (Vector2 point in points)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }
        }
    }



}


