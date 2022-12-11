using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private readonly float GravityConstant = 6.6743E-11f;
    private Body[] Bodies;
    private Vector3[] GravitationalForces;

    private void Start()
    {
        Bodies = GetComponentsInChildren<Body>();
        GravitationalForces = new Vector3[Bodies.Length];
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < Bodies.Length; i++)
        {
            var totalForce = Vector3.zero;
            var attractedBody = Bodies[i];
            foreach (var attractor in Bodies)
            {
                if (attractor == attractedBody) continue;
                var attractorPosition = attractor.transform.position;
                var attractedPosition = attractedBody.transform.position;
                var distanceBetween =
                    Math.Abs(Vector3.Distance(attractedPosition, attractorPosition));
                var force = GravityConstant * attractedBody.Mass * attractor.Mass /
                            Mathf.Pow(distanceBetween, 2f);
                var direction = attractorPosition - attractedPosition;
                totalForce += direction * force;
            }

            GravitationalForces[i] = totalForce;
            Debug.Log($"{attractedBody.name}: {totalForce.magnitude}N in direction {totalForce.normalized}");
        }

        for (var i = 0; i < Bodies.Length; i++) Bodies[i].ApplyForce(GravitationalForces[i]);
    }
}