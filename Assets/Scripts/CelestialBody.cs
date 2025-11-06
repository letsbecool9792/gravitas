using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass = 1e6f;
    public float radius = 1f;
    public Color color = Color.white;
    public Vector3 initialVelocity;  

    [HideInInspector] public Rigidbody rb;

    public void InitializeBody(float mass, float radius, Vector3 initialVelocity, Color color)
    {
        rb = GetComponent<Rigidbody>();

        this.mass = mass;
        this.radius = radius;
        rb.mass = mass;
        rb.velocity = initialVelocity;
        rb.WakeUp();

        transform.localScale = Vector3.one * radius;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            renderer.material.color = color;
            renderer.material.SetColor("_EmissionColor", color);
        }
    }

}
