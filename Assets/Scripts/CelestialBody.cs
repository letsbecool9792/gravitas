using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass = 1e6f;
    public float radius = 1f;
    public Color color = Color.white;
    public Vector3 initialVelocity;  // <-- new line

    [HideInInspector] public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;  // <-- apply here

        // optional: set material color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            renderer.material.color = color;
            renderer.material.SetColor("_EmissionColor", color);
        }
    }
}
