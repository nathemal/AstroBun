using System.Collections.Generic;
using UnityEngine;
using static PoissonDiscSampling;
using NUnit.Framework;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class PlanetSpawner : MonoBehaviour
{
        public float minPlanetSize = 1f;
        public float maxPlanetSize = 3f;
        public Vector2 regionSize = new Vector2(10, 10);
        public int rejectionSamples = 30;

        public GameObject planetPrefab;
        public PlanetSO[] planetTypes;

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

           /* List<PoissonPoint> points = PoissonDiscSampling.GeneratePoints(minPlanetSize, maxPlanetSize, regionSize, rejectionSamples);

            foreach (PoissonPoint point in points)
            {
                SpawnPlanet(point);
            }*/
        }

        void SpawnPlanet(PoissonPoint point)
        {
            if (planetPrefab == null || planetTypes.Length == 0)
            {
                Debug.LogError("Planet Prefab or Planet Types not assigned!");
                return;
            }

            GameObject planet = Instantiate(planetPrefab, point.position, Quaternion.identity);
            planet.transform.localScale = Vector3.one * point.radius; // Apply random scale
            spawnedPlanets.Add(planet);

            Planet planetComponent = planet.GetComponent<Planet>();
            if (planetComponent != null)
            {
                planetComponent.SetPlanetData(planetTypes[Random.Range(0, planetTypes.Length)]);
            }
            Debug.Log($"Spawned {planet.name} at {planet.transform.position} with scale {planet.transform.localScale}");
        }
    }
