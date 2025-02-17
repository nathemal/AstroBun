using UnityEngine;
using System.Collections.Generic;

public class PDGTest : MonoBehaviour 
{
    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejcetionSamples = 30;
    public float displayRadius = 1;

    List<Vector2> points;

    private void OnValidate()
    {
        points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejcetionSamples);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                Gizmos.DrawSphere(point, displayRadius);
            }
        }
    }
}