using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public float G = 6.67408e-11f;
    public float gravityScale = 1e6f;
    public float minDistance = 0.1f;

    void FixedUpdate()
    {
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody>();
        
        for (int i = 0; i < bodies.Length; i++)
        {
            for (int j = i + 1; j < bodies.Length; j++)
            {
                Vector3 r = bodies[j].transform.position - bodies[i].transform.position;
                float distSqr = Mathf.Max(r.sqrMagnitude, minDistance * minDistance);
                Vector3 forceDir = r.normalized;
                float forceMag = (G * bodies[i].mass * bodies[j].mass) / distSqr;
                Vector3 force = forceDir * forceMag * gravityScale;
                
                bodies[i].rb.AddForce(force);
                bodies[j].rb.AddForce(-force);
            }
        }
    }
}
