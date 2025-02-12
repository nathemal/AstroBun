using System.Collections.Generic;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    private static float gravityStrength = 1f; // For now you can't change gravity strength but it might be added in the future
    public static List<Rigidbody2D> attractors = new List<Rigidbody2D>();
    public static List<Rigidbody2D> attractees = new List<Rigidbody2D>();

    void FixedUpdate() // To avoid tying the game physics to fps FixedUpdate is used instead of Update here
    {
        SimulateGravity();
    }

    public static void SimulateGravity()
    {
        foreach (Rigidbody2D attractor in attractors)
        {
            foreach (Rigidbody2D attractee in attractees)
            {
                if (attractor != attractee)
                    AddGravityForce(attractor, attractee);
            }
        }
    }

    public static void AddGravityForce(Rigidbody2D attractor, Rigidbody2D target)
    {
        float massProduct = attractor.mass * target.mass * gravityStrength;

        Vector3 difference = attractor.position - target.position;
        float distance = difference.magnitude;

        float unscaledForceMagnitude = massProduct / Mathf.Pow(distance, 2);
        float forceMagnitude = gravityStrength * unscaledForceMagnitude;

        Vector3 forceDirection = difference.normalized;

        Vector3 forceVector = forceDirection * forceMagnitude;

        target.AddForce(forceVector);
    }
}
